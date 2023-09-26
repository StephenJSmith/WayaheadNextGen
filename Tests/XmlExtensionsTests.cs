using System.Xml.Linq;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class XmlExtensionsTests
{
  [Fact]
  public void ToDataDictionary_PropertyNOTIncludesDataAttributes_ReturnsEmptyDictionary()
  {
    var testElement = XElement.Parse("<Property Name=\"RCV_Instructions\" Description1=\"Picking Instructions\" Description2=\"\" CopyCommand=\"\" Hint=\"\" AutoCalculation=\"\" Validation=\"\" SecurityLevel=\"Disabled\" ESignCommentsRequired=\"N\" PreRenderScript=\"\" PostExecutionScript=\"\" ReferenceNo=\"\" CheckCompletion=\"Y\" CompletionErrorMessage=\"Please tick 'I have read Picking Instructions'.\" PostDeletionScript=\"\" EditorType=\"RichEditor\" DefaultValue=\"\" UOM=\"\" RunProgram1=\"\" RunProgram2=\"\" IframePosition=\"After\" ChildReport=\"\" FullSize=\"Y\" PictureEvidence=\"N\" Disable=\"Y\" Hide=\"N\" Nullable=\"N\" ReportType=\"Standard\" />");
    var expected = new Dictionary<string, string>();

    var actual = testElement.ToDataDictionary();

    actual.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public void ToDataDictionary_PropertyIncludesDataAttributes_ReturnsDictionaryWithAttributeValues()
  {
    var testElement = XElement.Parse("<Property Name=\"RCV_Instructions\" Description1=\"Picking Instructions\" Description2=\"\" CopyCommand=\"\" Hint=\"\" AutoCalculation=\"\" Validation=\"\" SecurityLevel=\"Disabled\" ESignCommentsRequired=\"N\" PreRenderScript=\"\" PostExecutionScript=\"\" ReferenceNo=\"\" CheckCompletion=\"Y\" CompletionErrorMessage=\"Please tick 'I have read Picking Instructions'.\" PostDeletionScript=\"\" EditorType=\"RichEditor\" DefaultValue=\"\" UOM=\"\" RunProgram1=\"\" RunProgram2=\"\" IframePosition=\"After\" ChildReport=\"\" FullSize=\"Y\" PictureEvidence=\"N\" Disable=\"Y\" Hide=\"N\" Nullable=\"N\" ReportType=\"Standard\" Data-RichEditor_Rows=\"3\" Data-RichEditor_FontSize=\"14\" />");
    var expected = new Dictionary<string, string>
    {
      {"RichEditor_Rows", "3" },
      {"RichEditor_FontSize", "14" }
    };

    var actual = testElement.ToDataDictionary();

    actual.Should().BeEquivalentTo(expected);
  }
}

