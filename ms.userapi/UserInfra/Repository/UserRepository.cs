using UserDomain.Entities;
using UserInfra.Repository;

namespace Repository
{
  public class UserRepository : IUserRepository
  {
    UserEntity IUserRepository.GetUserByEmail(string email)
    {
      throw new NotImplementedException();
    }
  }
}