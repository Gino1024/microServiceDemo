public class TUser
{
  public int UserId { get; set; }
  public string Name { get; set; }
  public string Mima { get; set; }
  public string Email { get; set; }
  public bool IsEnable { get; set; }
  public DateTime LastLoginAt { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime UpdateAt { get; set; }
  public ICollection<TUserFuncGroupRel> UserFuncGroupRels { get; set; }

}
