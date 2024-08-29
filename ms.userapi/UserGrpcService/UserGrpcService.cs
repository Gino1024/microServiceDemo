using System.ComponentModel.DataAnnotations;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ms.infrastructure.protos;
using StackExchange.Redis;
using UserDomain.Entities;
using UserInfra.Repository;
using ms.infrastructure.JWTHandler;

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

        public override async Task<GetUserByIdReply> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var reply = new GetUserByIdReply();
            reply.Result = new OperationResult();
            try
            {
                UserEntity user = await _userRepository.GetUserById(request.Id);

                if (user == null)
                {
                    reply.Result.Msg = "操作失敗: 無對應Id";
                    return reply;
                }

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
                reply.Result.Msg = "操作成功";
            }
            catch (Exception ex)
            {
                reply.Result.Msg = ex.ToString();
            }

            return reply;
        }
        public override async Task<GetUserByQueryReply> GetUserByQuery(GetUserByQueryRequest request, ServerCallContext context)
        {
            var reply = new GetUserByQueryReply();
            reply.Result = new OperationResult();

            return reply;
        }
        public override async Task<StandardReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            var reply = new StandardReply();
            reply.Result = new OperationResult();
            try
            {
                var isEmailExists = await _userRepository.GetUserByEmail(request.Email) != null;
                if (isEmailExists)
                {
                    reply.Result.Msg = "操作失敗: Email已被註冊";
                    return reply;
                }

                await _uniOfWork.BeginTrans();
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
                await _userRepository.RegisterUser(user_entity);
                await _uniOfWork.Commit();

                reply.Result.Msg = "操作成功";
                reply.Result.IsSuccess = true;
            }
            catch (Exception ex)
            {

                reply.Result.Msg = ex.ToString();
            }

            return reply;
        }
        public override async Task<StandardReply> EnableUser(GetUserByIdRequest request, ServerCallContext context)
        {
            var reply = new StandardReply();
            reply.Result = new OperationResult();
            try
            {
                UserEntity user = await _userRepository.GetUserById(request.Id);
                if (user == null)
                {
                    reply.Result.Msg = "操作失敗: 無對應Id";
                    return reply;
                }
                DateTime dtNow = DateTime.UtcNow;
                user.Enable(dtNow);
                await _userRepository.Enable(user);

                reply.Result.IsSuccess = true;
                reply.Result.Msg = "操作成功";
            }
            catch (Exception ex)
            {
                reply.Result.Msg = ex.ToString();
            }

            return reply;
        }
        public override async Task<StandardReply> DisableUser(GetUserByIdRequest request, ServerCallContext context)
        {
            var reply = new StandardReply();
            reply.Result = new OperationResult();
            try
            {
                UserEntity user = await _userRepository.GetUserById(request.Id);
                if (user == null)
                {
                    reply.Result.Msg = "操作失敗: 無對應Id";
                    return reply;
                }
                DateTime dtNow = DateTime.UtcNow;
                user.Disable(dtNow);
                await _userRepository.Disable(user);

                reply.Result.IsSuccess = true;
                reply.Result.Msg = "操作成功";
            }
            catch (Exception ex)
            {
                reply.Result.Msg = ex.ToString();
            }

            return reply;
        }
        public override async Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
        {
            var reply = new LoginReply();
            reply.Result = new OperationResult();
            try
            {
                UserEntity user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                {
                    reply.Result.Msg = "登入失敗: 無此使用者";
                    return reply;
                }

                if (request.Mima != user.mima)
                {
                    reply.Result.Msg = "登入失敗: 請確認是否輸入正確";
                    return reply;
                }

                //建立JWT Token
                reply.Result.IsSuccess = true;
                reply.Result.Msg = "登入成功";
                reply.Data = JWTHandler.GenerateJwtToken(user.email);
            }
            catch (Exception ex)
            {
                reply.Result.Msg = ex.ToString();
            }

            return reply;
        }

    }
}