public class TFuncGroupRel
{
  public int FuncGroupID { get; set; }
  public int FuncID { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime UpdateAt { get; set; }
  public TFunction Func { get; set; }
  public TFuncGroup FuncGroup { get; set; }
}
