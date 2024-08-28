using Microsoft.EntityFrameworkCore.Storage;
using UserInfra.Repository;

namespace Repository
{
  public class UniOfWork : IUniOfWork
  {
    private readonly MicroServiceDbContext _dbContext;
    private IDbContextTransaction _transaction;
    public UniOfWork(MicroServiceDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public void SaveChange()
    {
      _dbContext.SaveChanges();
    }

    public void BeginTrans()
    {
      _transaction = _dbContext.Database.BeginTransaction();
    }
    public void Commit()
    {
      try
      {
        _dbContext.SaveChanges();  // 保存所有更改
        _transaction?.Commit();  // 提交事务
      }
      catch
      {
        Rollback();
        throw;
      }
    }

    public void Rollback()
    {
      _transaction?.Rollback();  // 回滚事务
    }
  }
}