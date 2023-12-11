using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
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

  [Fact]
  public void ConvertXElementToXmlNode()
  {
    var xElement = XElement.Load(TestPathFile);
    using var xmlReader = xElement.CreateReader();
    var xmlDoc = new XmlDocument();
    xmlDoc.Load(xmlReader);

  }

  [Fact]
  public void ConvertToJson()
  {
    var doc = new XmlDocument();
    // doc.LoadXml(TestPathFile);

    var actual = JsonConvert.SerializeXmlNode(doc);
  }

  [Fact]
  public void ConvertToJsonViaXElement()
  {
    var xElement = XElement.Load(TestPathFile);
    xElement.
    var xmlNode = GetXmlNode(xElement));
    var actual = JsonConvert.SerializeXmlNode(xmlNode);
  }

  private XmlNode GetXmlNode(XElement xElement)
  {
    using var xmlReader = xElement.CreateReader();
    var xmlDoc = new XmlDocument();
    xmlDoc.Load(xmlReader);

    return xmlDoc;
  }
}