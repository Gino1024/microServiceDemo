using UserDomain.Entities;

namespace UserInfra.Repository
{
  public interface IUserRepository
  {
    public UserEntity? GetUserById(int id);
    public UserEntity? GetUserByEmail(string email);
    public void RegisterUser(UserEntity user_entity);
    public void Enable(UserEntity user_entity);
    public void Disable(UserEntity user_entity);
  }
}