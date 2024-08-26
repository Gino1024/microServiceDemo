namespace UserDomain.Entities
{
  public class UserEntity
  {
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mima { get; set; }
    public DateTime MimaChangeAt { get; set; }
    public bool IsEnable { get; set; }
    public DateTime LastLoginAt { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }

    /// <summary>
    /// 阻止外部直接new出實例
    /// </summary>
    private UserEntity() { }
    /// <summary>
    /// 建立User實例
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    public static UserEntity Create(int userId, string name, string email, string mima, DateTime mimaChangeAt, bool isEnable, DateTime lastLoginAt, DateTime createAt, DateTime updateAt)
    {
      return new UserEntity
      {
        UserId = userId,
        Name = name,
        Email = email,
        Mima = mima,
        MimaChangeAt = mimaChangeAt,
        IsEnable = isEnable,
        LastLoginAt = lastLoginAt,
        CreateAt = createAt,
        UpdateAt = updateAt,
      };
    }
    public void ChangeEmail(string email, DateTime dtNow)
    {
      Email = email;
      UpdateAt = DateTime.Now;
    }
    public void ChangeMima(string mima, DateTime dtNow)
    {
      Mima = mima;
      MimaChangeAt = dtNow;
      UpdateAt = DateTime.Now;
    }
    public void ChangeLastLoginAt(DateTime dtNow)
    {
      LastLoginAt = dtNow;
    }
    public void Enable(DateTime dtNow)
    {
      IsEnable = true;
      UpdateAt = dtNow;
    }
    public void Disable(DateTime dtNow)
    {
      IsEnable = false;
      UpdateAt = dtNow;
    }
    public bool ValidateMima(string mima)
    {
      bool result = Mima == mima;
      return result;
    }
  }
}