public class TUserFuncGroupRel
{
  public int FuncGroupID { get; set; }
  public int UserID { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime UpdateAt { get; set; }
  public TUser User { get; set; }
  public TFuncGroup FuncGroup { get; set; }
}
