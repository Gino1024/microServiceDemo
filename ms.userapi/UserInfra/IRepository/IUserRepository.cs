using UserDomain.Entities;

namespace UserInfra.Repository
{
  public interface IUserRepository
  {
    public UserEntity GetUserByEmail(string email);
  }
}