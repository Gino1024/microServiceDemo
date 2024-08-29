namespace UserInfra.Repository
{
  public interface IUniOfWork
  {
    public Task SaveChange();
    public Task BeginTrans();
    public Task Commit();
    public Task Rollback();
  }
}