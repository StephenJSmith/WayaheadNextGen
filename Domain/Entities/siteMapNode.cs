namespace Domain.Entities;

public class siteMapNode 
{
  public string SystemName { get; set; }
  public string Resource { get; set; }
  public string Controller { get; set; }
  public string Action { get; set; }
  public string IconClass { get; set; }
  public string InfoFlag { get; set; }
  public string Url { get; set; }
  public List<siteMapNode> SiteMapNodes { get; set; }
}
