using Microsoft.EntityFrameworkCore;
using UserDomain.Entities;
using UserInfra.Repository;

namespace Repository
{
  public class UserRepository : IUserRepository
  {
    private readonly MicroServiceDbContext _dbContext;
    public UserRepository(MicroServiceDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public UserEntity? GetUserByEmail(string email)
    {
      var user = _dbContext.TUser.Where(m => m.email == email).FirstOrDefault();
      if (user == null)
        return null;

      var user_entity = UserEntity.Create(
        user.user_id,
        user.name,
        user.email,
        user.mima,
        user.mima_change_at,
        user.is_enable,
        user.last_login_at,
        user.create_at,
        user.update_at
      );

      return user_entity;
    }

    public UserEntity? GetUserById(int id)
    {
      var user = _dbContext.TUser.Where(m => m.user_id == id).AsNoTracking().FirstOrDefault();
      if (user == null)
        return null;

      var user_entity = UserEntity.Create(
        user.user_id,
        user.name,
        user.email,
        user.mima,
        user.mima_change_at,
        user.is_enable,
        user.last_login_at,
        user.create_at,
        user.update_at
      );

      return user_entity;
    }

    public void RegisterUser(UserEntity user_entity)
    {
      TUser user = new TUser()
      {
        user_id = user_entity.user_id,
        name = user_entity.name,
        email = user_entity.email,
        mima = user_entity.mima,
        mima_change_at = user_entity.mima_change_at,
        is_enable = user_entity.is_enable,
        last_login_at = user_entity.last_login_at,
        create_at = user_entity.create_at,
        update_at = user_entity.update_at,
      };

      _dbContext.TUser.Add(user);
      _dbContext.SaveChanges();
      user_entity.SetUserId(user.user_id);
    }
    public void Disable(UserEntity user_entity)
    {
      var user = new TUser { user_id = user_entity.user_id, is_enable = false, update_at = user_entity.update_at };
      _dbContext.Attach(user);
      _dbContext.Entry(user).Property(e => e.is_enable).IsModified = true;
      _dbContext.Entry(user).Property(e => e.update_at).IsModified = true;

      user.is_enable = false;
      _dbContext.SaveChanges();
    }

    public void Enable(UserEntity user_entity)
    {
      var user = new TUser { user_id = user_entity.user_id, is_enable = true, update_at = user_entity.update_at };
      _dbContext.Attach(user);
      _dbContext.Entry(user).Property(e => e.is_enable).IsModified = true;
      _dbContext.Entry(user).Property(e => e.update_at).IsModified = true;

      _dbContext.SaveChanges();
    }
  }
}