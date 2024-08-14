using Grpc.Core;
using ms.infrastructure.protos;

namespace ms.user.Services
{
    public class UserService : User.UserBase
    {
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> GetList(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Nice to meet you, " + request.Name
            });
        }
    }
}
