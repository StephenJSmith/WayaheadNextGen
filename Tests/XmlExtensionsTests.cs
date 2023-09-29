using System.IO;
using System.Xml.Linq;
using Domain.Enums;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class XmlExtensionsTests
{
  [Fact]
  public void ToStringValue_WhenValidAttribute_ThenAttributeValue()
  {
    var testValue = "HERBAL REVIVAL FACE CREAM";
    var testAttribute = new XAttribute("Description1", testValue);
    var expected = testValue;

    var actual = testAttribute?.ToStringValue();

    actual.Should().Be(expected);
  }

    [Fact]
    public void ToStringValue_WhenEmptyStringValue_ThenEmptyStringValue()
    {
        var testValue = "";
        var testAttribute = new XAttribute("Description1", testValue);
        var expected = string.Empty;

        var actual = testAttribute?.ToStringValue();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToDateTime_WhenValidAttribute_ThenDateTime()
    {
        var testValue = "2022-05-15 16:35:18";
        var testAttribute = new XAttribute("SavedOn", testValue);
        var expected = DateTime.Parse(testValue);

        var actual = testAttribute.ToDateTime();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToDateTime_WhenEmptyAttribute_ThenMinDateTime()
    {
        var testValue = "";
        var testAttribute = new XAttribute("SavedOn", testValue);
        var expected = DateTime.MinValue;

        var actual = testAttribute.ToDateTime();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToTimeSpan_WhenValidAttributeValue_ThenExpectedTimeSpanValue()
    {
        var testValue = "00:05:30";
        var testAttribute = new XAttribute("RepeatValue", testValue);
        var expected = TimeSpan.Parse(testValue);

        var actual = testAttribute.ToTimeSpan();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToTimeSpan_WhenInvalidAttributeValue_ThenTimeSpanZero()
    {
        var testValue = "";
        var testAttribute = new XAttribute("RepeatValue", testValue);
        var expected = TimeSpan.Zero;

        var actual = testAttribute.ToTimeSpan();

        actual.Should().Be(expected);
    }

    [Fact]
    public void FromHierarchicalNumber_WhenValidAttributeWithDotSeparators_ThenNumberAfterFinalDot()
    {
        var testValue = "1.2.4";
        var testAttribute = new XAttribute("Number", testValue);
        var expected = 4;

        var actual = testAttribute.FromHierarchicalNumber();

        actual.Should().Be(expected);
    }

    [Fact]
    public void FromHierarchicalNumber_WhenValidAttributeWithOUTDotSeparators_ThenNumberValue()
    {
        var testValue = "3";
        var testAttribute = new XAttribute("Number", testValue);
        var expected = 3;

        var actual = testAttribute.FromHierarchicalNumber();

        actual.Should().Be(expected);
    }

    [Fact]
    public void FromHierarchicalNumber_WhenEmptyString_ThenZero()
    {
        var testValue = "";
        var testAttribute = new XAttribute("Number", testValue);
        var expected = 0;

        var actual = testAttribute.FromHierarchicalNumber();

        actual.Should().Be(expected);
    }

    [Fact]
    public void FromHierarchicalNumber_WhenNonNumericString_ThenZero()
    {
        var testValue = "Uno";
        var testAttribute = new XAttribute("Number", testValue);
        var expected = 0;

        var actual = testAttribute.FromHierarchicalNumber();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToEditorType_WhenValidEnumValueAsString_ThenEnumValue()
    {
        var testValue = "Checkbox";
        var testAttribute = new XAttribute("EditorType", testValue);
        var expected = MesEditorType.Checkbox;

        var actual = testAttribute.ToEditorType();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToEditorType_WhenInvalidEnumValue_ThenTextStringEnum()
    {
        var testValue = "Invalid Value";
        var testAttribute = new XAttribute("EditorType", testValue);
        var expected = MesEditorType.TextString;

        var actual = testAttribute.ToEditorType();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToEditorType_WhenEmptyString_ThenTextStringEnumAsDefault()
    {
        var testValue = string.Empty;
        var testAttribute = new XAttribute("EditorType", testValue);
        var expected = MesEditorType.TextString;

        var actual = testAttribute.ToEditorType();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToSecurityLevel_WhenValidEnumValueAsString_ThenEnumValue()
    {
        var testValue = "Enabled";
        var testAttribute = new XAttribute("SecurityLevel", testValue);
        var expected = SecurityLevel.Enabled;

        var actual = testAttribute.ToSecurityLevel();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToSecurityLevel_WhenInvalidEnumValue_ThenDisabledEnumValueAsDefault()
    {
        var testValue = "Trustworthy";
        var testAttribute = new XAttribute("SecurityLevel", testValue);
        var expected = SecurityLevel.Disabled;

        var actual = testAttribute.ToSecurityLevel();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToSecurityLevel_WhenEmptyString_ThenDisabledEnumValueAsDefault()
    {
        var testValue = string.Empty;
        var testAttribute = new XAttribute("SecurityLevel", testValue);
        var expected = SecurityLevel.Disabled;

        var actual = testAttribute.ToSecurityLevel();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToReportCsvStyle_WhenValidReportCsvStyleValueAsString_ThenEnumValue()
    {
        var testValue = "Horizontal";
        var testAttribute = new XAttribute("ReportCsvStyle", testValue);
        var expected = MesReportCsvStyle.Horizontal;

        var actual = testAttribute.ToReportCsvStyle();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToReportCsvStyle_WhenInvalidReportCsvStyleValue_ThenVertialAsDefault()
    {
        var testValue = "Triangle";
        var testAttribute = new XAttribute("ReportCsvStyle", testValue);
        var expected = MesReportCsvStyle.Vertical;

        var actual = testAttribute.ToReportCsvStyle();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToReportCsvStyle_WhenEmptyStringValue_ThenVertialAsDefault()
    {
        var testValue = string.Empty;
        var testAttribute = new XAttribute("ReportCsvStyle", testValue);
        var expected = MesReportCsvStyle.Vertical;

        var actual = testAttribute.ToReportCsvStyle();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToReportType_WhenValidValueAsString_ThenEnumValue()
    {
        var testValue = "CSV";
        var testAttribute = new XAttribute("ReportType", testValue);
        var expected = MesReportType.Csv;

        var actual = testAttribute.ToReportType();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToReportType_WhenInvalidValue_ThenStandardAsDefault()
    {
        var testValue = "Comma Separated Value";
        var testAttribute = new XAttribute("ReportType", testValue);
        var expected = MesReportType.Standard;

        var actual = testAttribute.ToReportType();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToReportType_WhenEmptyString_ThenStandardAsDefault()
    {
        var testValue = string.Empty;
        var testAttribute = new XAttribute("ReportType", testValue);
        var expected = MesReportType.Standard;

        var actual = testAttribute.ToReportType();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToIFramePosition_WhenValidValueAsString_ThenEnumValue()
    {
        var testValue = "Before";
        var testAttribute = new XAttribute("IframePosition", testValue);
        var expected = MesIFramePosition.Before;

        var actual = testAttribute.ToIFramePosition();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToIFramePosition_WhenInvalidValue_ThenAfterAsDefault()
    {
        var testValue = "LeftHandSide";
        var testAttribute = new XAttribute("IframePosition", testValue);
        var expected = MesIFramePosition.After;

        var actual = testAttribute.ToIFramePosition();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToIFramePosition_WhenEmptyString_ThenAfterAsDefault()
    {
        var testValue = string.Empty;
        var testAttribute = new XAttribute("IframePosition", testValue);
        var expected = MesIFramePosition.After;

        var actual = testAttribute.ToIFramePosition();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToInt_WhenValidValueAsString_ThenIntegerVaue()
    {
        var testValue = "42";
        var testAttribute = new XAttribute("FontSize", testValue);
        var expected = 42;

        var actual = testAttribute.ToInt();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToInt_WhenInvalidValue_ThenZeroAsDefault() {
        var testValue = "Forty-Two";
        var testAttribute = new XAttribute("FontSize", testValue);
        var expected = 0;

        var actual = testAttribute.ToInt();

        actual.Should().Be(expected);
    }

    [Fact]
    public void ToInt_WhenEmptyString_ThenZeroAsDefault()
    {
        var testValue = string.Empty;
        var testAttribute = new XAttribute("FontSize", testValue);
        var expected = 0;

        var actual = testAttribute.ToInt();

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("y", true)]
    [InlineData("Y", true)]
    [InlineData("yes", true)]
    [InlineData("yES", true)]
    [InlineData("true", true)]
    [InlineData("TRUE", true)]
    [InlineData("N", false)]
    [InlineData("no", false)]
    [InlineData("", false)]
    [InlineData("si", false)]

    public void ToBoolean_WhenValue_ThenExpectedBoolean(string testValue, bool expected)
    {
        var testAttribute = new XAttribute("Hide", testValue);

        var actual = testAttribute.ToBoolean();

        actual.Should().Be(expected);   
    }

    [Fact]
  public void ToDataDictionary_WhenPropertyNOTIncludesDataAttributes_ReturnsEmptyDictionary()
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

