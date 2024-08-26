public class TFunction
{
  public int FuncID { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Url { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime UpdateAt { get; set; }
  public ICollection<TFuncGroupRel> FuncGroupRels { get; set; }
}
