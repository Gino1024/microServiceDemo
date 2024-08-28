using System.ComponentModel.DataAnnotations;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ms.infrastructure.protos;
using StackExchange.Redis;
using UserDomain.Entities;
using UserInfra.Repository;

namespace ms.user.Services
{
    public class UserGrpcService : UserProto.UserProtoBase
    {
        private readonly ILogger<UserGrpcService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUniOfWork _uniOfWork;
        public UserGrpcService(ILogger<UserGrpcService> logger, IUserRepository userRepository, IUniOfWork uniOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _uniOfWork = uniOfWork;
        }

        public override Task<GetUserByIdReply> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var reply = new GetUserByIdReply();
            reply.Result = new OperationResult();
            try
            {
                UserEntity user = _userRepository.GetUserById(request.Id);

                if (user == null)
                {
                    reply.Result.Msg = "操作失敗: 無對應Id";
                }
                else
                {
                    reply.Data = new UserReplyDto();
                    reply.Data.UserId = user.user_id;
                    reply.Data.Name = user.name;
                    reply.Data.Email = user.email;
                    reply.Data.MimaChangeAt = user.mima_change_at.HasValue ? user.mima_change_at.Value.ToTimestamp() : null;
                    reply.Data.IsEnable = user.is_enable;
                    reply.Data.LastLoginAt = user.last_login_at.HasValue ? user.last_login_at.Value.ToTimestamp() : null;
                    reply.Data.CreateAt = user.create_at.HasValue ? user.create_at.Value.ToTimestamp() : null;
                    reply.Data.UpdateAt = user.update_at.HasValue ? user.update_at.Value.ToTimestamp() : null;

                    reply.Result.IsSuccess = true;
                    reply.Result.Msg = "查詢成功";
                }
            }
            catch (Exception ex)
            {
                reply.Result.Msg = ex.ToString();
            }

            return Task.FromResult(reply);
        }
        public override Task<GetUserByQueryReply> GetUserByQuery(GetUserByQueryRequest request, ServerCallContext context)
        {
            var reply = new GetUserByQueryReply();
            reply.Result = new OperationResult();

            return Task.FromResult(reply);
        }
        public override Task<StandardReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            var reply = new StandardReply();
            reply.Result = new OperationResult();
            try
            {
                var isEmailExists = _userRepository.GetUserByEmail(request.Email) != null;
                if (isEmailExists)
                {
                    reply.Result.Msg = "Email已被註冊";
                }
                else
                {
                    _uniOfWork.BeginTrans();
                    DateTime dtNow = DateTime.UtcNow;

                    var user_entity = UserEntity.Create(
                        0,
                        request.Name,
                        request.Email,
                        request.Mima,
                        dtNow,
                        true,
                        null,
                        dtNow,
                        dtNow
                    );
                    _userRepository.RegisterUser(user_entity);
                    _uniOfWork.Commit();

                    reply.Result.Msg = "新增成功";
                }
                reply.Result.IsSuccess = true;
            }
            catch (Exception ex)
            {

                reply.Result.Msg = ex.ToString();
            }

            return Task.FromResult(reply);
        }

        public override Task<StandardReply> EnableUser(GetUserByIdRequest request, ServerCallContext context)
        {
            var reply = new StandardReply();
            reply.Result = new OperationResult();
            try
            {
                UserEntity user = _userRepository.GetUserById(request.Id);
                if (user == null)
                {
                    reply.Result.Msg = "操作失敗: 無對應Id";
                }
                else
                {
                    DateTime dtNow = DateTime.UtcNow;
                    user.Enable(dtNow);
                    _userRepository.Enable(user);

                    reply.Result.IsSuccess = true;
                    reply.Result.Msg = "操作成功";
                }
            }
            catch (Exception ex)
            {
                reply.Result.Msg = ex.ToString();
            }

            return Task.FromResult(reply);
        }
        public override Task<StandardReply> DisableUser(GetUserByIdRequest request, ServerCallContext context)
        {
            var reply = new StandardReply();
            reply.Result = new OperationResult();
            try
            {
                UserEntity user = _userRepository.GetUserById(request.Id);
                if (user == null)
                {
                    reply.Result.Msg = "操作失敗: 無對應Id";
                }
                else
                {
                    DateTime dtNow = DateTime.UtcNow;
                    user.Disable(dtNow);
                    _userRepository.Disable(user);

                    reply.Result.IsSuccess = true;
                    reply.Result.Msg = "操作成功";
                }
            }
            catch (Exception ex)
            {
                reply.Result.Msg = ex.ToString();
            }

            return Task.FromResult(reply);
        }
    }
}
