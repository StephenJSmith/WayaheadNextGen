using System.Xml;
using System.Xml.Linq;
using FluentAssertions;
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
    var xmlNode = GetXmlNode(xElement);
    var actual = JsonConvert.SerializeXmlNode(xmlNode);
  }

  [Fact]
  public void GetNextAvailableChildBatchSeq()
  {
    var batches = new List<string> {
      "A1234-2_3",
      "A1234-2_2",
      "A1234-2_1",
      "A1234-2_6",
      "A1234-2_5",
      "A1234-2_4",
    };
    var expected = 7;

    var maxNumber = batches
      .Select(x => Convert.ToInt32(x.Substring(x.LastIndexOf('_')+1)))
      .Max();
    var actual = maxNumber + 1;

    actual.Should().Be(expected);
  }

  private XmlNode GetXmlNode(XElement xElement)
  {
    using var xmlReader = xElement.CreateReader();
    var xmlDoc = new XmlDocument();
    xmlDoc.Load(xmlReader);

    return xmlDoc;
  }
}