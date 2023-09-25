using System.Xml.Linq;
using Domain.Enums;

namespace Persistence.Repositories;

public static class XmlExtensions {
  public static bool IsYN(this string value) {
    if (string.IsNullOrWhiteSpace(value)) return false;

    return value.ToUpper() == "Y";
  }

  public static int FromHierarchicalNumber(this string value) {
    if (string.IsNullOrWhiteSpace(value)) return 0;

    var elements = value.Trim().Split(".");
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

    return attribute.Value.ToUpper() == "Y";
  }
}