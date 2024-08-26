public class TFuncGroup
{
  public int FuncGroupID { get; set; }
  public string Name { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime UpdateAt { get; set; }
  public ICollection<TFuncGroupRel> FuncGroupRels { get; set; }
  public ICollection<TUserFuncGroupRel> UserFuncGroupRels { get; set; }
}
