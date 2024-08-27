public class TFunction
{
  public int func_id { get; set; }
  public string name { get; set; }
  public string description { get; set; }
  public string url { get; set; }
  public DateTime create_at { get; set; }
  public DateTime update_at { get; set; }
  public ICollection<TFuncGroupRel> func_group_rels { get; set; }
}
