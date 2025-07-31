namespace allspice_cs.Models;

public class Recipe : DbItem<int>
{
  public string Title { get; set; }
  public string instructions { get; set; }
  public string ImgUrl { get; set; }
  public string Category { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}