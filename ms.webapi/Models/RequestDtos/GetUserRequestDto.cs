namespace ms.webapi.Models
{
  public class GetUserByQueryRequestDto
  {
    public List<string> group { get; set; }
    public string name { get; set; }
    public string order_by { get; set; }
  }
}