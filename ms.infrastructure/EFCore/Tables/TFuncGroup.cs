public class TFuncGroup
{
  public int func_group_id { get; set; }
  public string name { get; set; }
  public DateTime? create_at { get; set; }
  public DateTime? update_at { get; set; }
  public ICollection<TFuncGroupRel> func_group_rels { get; set; }
  public ICollection<TUserFuncGroupRel> user_func_group_rels { get; set; }
}
