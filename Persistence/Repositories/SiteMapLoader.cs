using System.Xml;
using System.Xml.Linq;
using Domain.Entities;

namespace Persistence.Repositories;

public class SiteMapLoader 
{
  public siteMap GetSiteMap(string siteMapPathFile)
  {
    if (!Path.Exists(siteMapPathFile)) throw new FileNotFoundException();

    try
    {
      var xElement = XElement.Load(siteMapPathFile);

      return new siteMap();
    }
    catch (System.Exception)
    {
      
      throw new XmlException();
    }
  }
}