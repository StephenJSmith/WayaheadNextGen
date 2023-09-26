using System.Xml.Linq;
using Domain.Enums;

namespace Persistence.Repositories;

public static class XmlExtensions {

  public static string ToStringValue(this XAttribute attribute) {
    if (attribute == null) return string.Empty;

    return attribute.Value ?? string.Empty;
  }

  public static DateTime ToDateTime(this XAttribute attribute) {
    if (attribute == null) return DateTime.MinValue;

    if (!DateTime.TryParse(attribute.Value, out DateTime parsedDateTime)) {
      return DateTime.MinValue;
    }

    return parsedDateTime;
  }

  public static int FromHierarchicalNumber(this XAttribute attribute) {
    if (attribute == null 
      || string.IsNullOrWhiteSpace(attribute.Value)) return 0;

    var elements = attribute.Value.Trim().Split(".");
    if (!elements.Any()) return 0;

    var lastValue = elements[elements.Length - 1];

    if (!int.TryParse(lastValue, out int extractedNumber)){
      return 0;
    };

    return extractedNumber;
  }

  public static MesEditorType ToEditorType(this string value) {
    var editorType = (MesEditorType)Enum.Parse(typeof(MesEditorType), value, true);

    return editorType;
  }

  public static MesIFramePosition ToIFramePosition(this string value) {
    var iFramePosition = (MesIFramePosition)Enum.Parse(typeof(MesIFramePosition), value, true);

    return iFramePosition;
  }

  public static MesReportType ToReportType(this string value) {
    var reportType = (MesReportType)Enum.Parse(typeof(MesReportType), value, true);

    return reportType;
  }

  public static int ToInt(this XAttribute attribute) {
    if (attribute == null) return 0;

    if (!int.TryParse(attribute.Value, out int value)) {
      return 0;
    }

    return value;
  }

  public static bool ToBoolean(this XAttribute attribute) {
    if (attribute == null) return false;

    var trueValues = new string[] {"y", "yes", "true"};
    var result = trueValues.Any(x => x == attribute.Value.ToLower());

    return result;
  }
}