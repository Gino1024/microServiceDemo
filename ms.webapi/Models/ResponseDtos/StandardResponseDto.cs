namespace ms.webapi.Models
{
  public class StandardResponseDto<T>
  {
    public bool is_success { get; set; } = false;
    public string msg { get; set; } = string.Empty;
    public T data { get; set; }
  }
}