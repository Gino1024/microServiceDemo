namespace UserDomain.Entities
{
  public class UserEntity
  {
    public int user_id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string mima { get; set; }
    public DateTime? mima_change_at { get; set; }
    public bool is_enable { get; set; }
    public DateTime? last_login_at { get; set; }
    public DateTime? create_at { get; set; }
    public DateTime? update_at { get; set; }

    /// <summary>
    /// 阻止外部直接new出實例
    /// </summary>
    private UserEntity() { }
    public void SetUserId(int id)
    {
      this.user_id = id;
    }
    /// <summary>
    /// 建立User實例
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    public static UserEntity Create(int userId, string name, string email, string mima, DateTime? mimaChangeAt, bool isEnable, DateTime? lastLoginAt, DateTime? createAt, DateTime? updateAt)
    {
      return new UserEntity
      {
        user_id = userId,
        name = name,
        email = email,
        mima = mima,
        mima_change_at = mimaChangeAt,
        is_enable = isEnable,
        last_login_at = lastLoginAt,
        create_at = createAt,
        update_at = updateAt,
      };
    }
    public void ChangeEmail(string email, DateTime dtNow)
    {
      email = email;
      update_at = dtNow;
    }
    public void ChangeMima(string mima, DateTime dtNow)
    {
      mima = mima;
      mima_change_at = dtNow;
      update_at = DateTime.Now;
    }
    public void ChangeLastLoginAt(DateTime dtNow)
    {
      last_login_at = dtNow;
    }
    public void Enable(DateTime dtNow)
    {
      is_enable = true;
      update_at = dtNow;
    }
    public void Disable(DateTime dtNow)
    {
      is_enable = false;
      update_at = dtNow;
    }
    public bool ValidateMima(string mima)
    {
      bool result = (this.mima == mima);
      return result;
    }
  }
}