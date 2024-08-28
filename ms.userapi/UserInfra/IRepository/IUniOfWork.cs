namespace UserInfra.Repository
{
  public interface IUniOfWork
  {
    public void SaveChange();
    public void BeginTrans();
    public void Commit();
    public void Rollback();
  }
}