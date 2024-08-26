using UserInfra.Repository;

namespace Repository
{
  public class UniOfWork : IUniOfWork
  {
    private readonly MicroServiceDbContext _dbContext;
    public UniOfWork(MicroServiceDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public void SaveChange()
    {
      _dbContext.SaveChanges();
    }
  }
}