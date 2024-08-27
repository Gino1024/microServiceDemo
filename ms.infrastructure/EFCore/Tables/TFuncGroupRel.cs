public class TFuncGroupRel
{
  public int func_group_id { get; set; }
  public int func_id { get; set; }
  public DateTime create_at { get; set; }
  public DateTime update_at { get; set; }
  public TFunction func { get; set; }
  public TFuncGroup func_group { get; set; }
}
