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
    public async Task SaveChange()
    {
      await _dbContext.SaveChangesAsync();
    }

    public async Task BeginTrans()
    {
      _transaction = await _dbContext.Database.BeginTransactionAsync();
    }
    public async Task Commit()
    {
      try
      {
        await _dbContext.SaveChangesAsync(); // 保存所有更改
        await _transaction?.CommitAsync();  // 提交事务
      }
      catch
      {
        await Rollback();
        throw;
      }
    }

    public async Task Rollback()
    {
      await _transaction?.RollbackAsync();  // 回滚事务
    }
  }
}