using Persistence.Repositories;

namespace Tests;

public class SiteMapLoaderTests
{
  private const string TestPathFile = "sitemap.config";

  [Fact]
  public void LoadSiteMap()
  {
   var sut = new SiteMapLoader();

   var actual = sut.GetSiteMap(TestPathFile); 
  }
}