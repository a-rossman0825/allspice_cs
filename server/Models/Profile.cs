namespace allspice_cs.Models;

public class Profile : DbItem<string>
{
  public string Name { get; set; }
  public string Picture { get; set; }
}