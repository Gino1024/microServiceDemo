using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ms.infrastructure.protos;
using StackExchange.Redis;

namespace ms.user.Services
{
    public class UserGrpcService : UserProto.UserProtoBase
    {
        private readonly ILogger<UserGrpcService> _logger;
        private List<UserVO> _users;
        public UserGrpcService(ILogger<UserGrpcService> logger)
        {
            _logger = logger;
            _users = new List<UserVO>(){
                new UserVO() {id = 0, name = "gino", email = "gino@gmail.com" },
                new UserVO() {id = 1, name = "david", email = "david@gmail.com" },
                new UserVO() {id = 2, name = "marry", email = "marry@gmail.com" },
            };
        }

        public override Task<UsersReply> GetList(Empty request, ServerCallContext context)
        {
            var result = new UsersReply();

            foreach (var item in _users)
            {
                var user = new UserDto()
                {
                    Id = item.id,
                    Name = item.name,
                    Email = item.email
                };

                result.Data.Add(user);
            }
            return Task.FromResult(result);
        }
        public override Task<UserReply> Get(UserRequest request, ServerCallContext context)
        {
            var result = new UserReply();
            var user = _users.FirstOrDefault(m => m.id == request.Id);
            if (user == null)
                return null;

            result.Data = new UserDto()
            {
                Id = user.id,
                Name = user.name,
                Email = user.email
            };

            return Task.FromResult(result);
        }
    }
}
