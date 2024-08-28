public class TUser
{
  public int user_id { get; set; }
  public string name { get; set; }
  public string mima { get; set; }
  public DateTime? mima_change_at { get; set; }
  public string email { get; set; }
  public bool is_enable { get; set; }
  public DateTime? last_login_at { get; set; }
  public DateTime? create_at { get; set; }
  public DateTime? update_at { get; set; }
  public ICollection<TUserFuncGroupRel> user_func_group_rels { get; set; }

}
