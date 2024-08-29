using UserDomain.Entities;

namespace UserInfra.Repository
{
  public interface IUserRepository
  {
    public Task<UserEntity?> GetUserById(int id);
    public Task<UserEntity?> GetUserByEmail(string email);
    public Task RegisterUser(UserEntity user_entity);
    public Task Enable(UserEntity user_entity);
    public Task Disable(UserEntity user_entity);
  }
}