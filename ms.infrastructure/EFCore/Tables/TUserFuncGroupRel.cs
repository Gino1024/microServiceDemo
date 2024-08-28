public class TUserFuncGroupRel
{
  public int func_group_id { get; set; }
  public int user_id { get; set; }
  public DateTime? create_at { get; set; }
  public DateTime? update_at { get; set; }
  public TUser user { get; set; }
  public TFuncGroup func_group { get; set; }
}
