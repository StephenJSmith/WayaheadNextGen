using System.Xml.Linq;
using Domain.Enums;

namespace Persistence.Repositories;

public static class XmlExtensions
{

  public static string ToStringValue(this XAttribute attribute)
  {
    if (attribute == null) return string.Empty;

    return attribute.Value ?? string.Empty;
  }

  public static DateTime ToDateTime(this XAttribute attribute)
  {
    if (attribute == null) return DateTime.MinValue;

    if (!DateTime.TryParse(attribute.Value, out DateTime parsedDateTime))
    {
      return DateTime.MinValue;
    }

    return parsedDateTime;
  }

  public static TimeSpan ToTimeSpan(this XAttribute attribute)
  {
    if (attribute == null) return TimeSpan.Zero;

    if (!TimeSpan.TryParse(attribute.Value, out TimeSpan parsedTimeSpan))
    {
        return TimeSpan.Zero;
    }

    return parsedTimeSpan;
  }

  public static int FromHierarchicalNumber(this XAttribute attribute)
  {
    if (attribute == null
      || string.IsNullOrWhiteSpace(attribute.Value)) return 0;

    var elements = attribute.Value.Trim().Split(".");
    if (!elements.Any()) return 0;

    var lastValue = elements[elements.Length - 1];

    if (!int.TryParse(lastValue, out int extractedNumber))
    {
      return 0;
    };

    return extractedNumber;
  }

  public static MesEditorType ToEditorType(this XAttribute attribute)
  {
    if (attribute == null
      || string.IsNullOrWhiteSpace(attribute.Value)) return MesEditorType.TextString;

    try
    {
      var editorType = (MesEditorType)Enum.Parse(typeof(MesEditorType), attribute.Value, true);

      return editorType;
    }
    catch (Exception)
    {
      return MesEditorType.TextString;
    }
  }

  public static SecurityLevel ToSecurityLevel(this XAttribute attribute)
  {
    if (attribute == null
      || string.IsNullOrWhiteSpace(attribute.Value)) return SecurityLevel.Disabled;

    try
    {
      var result = (SecurityLevel)Enum.Parse(typeof(SecurityLevel), attribute.Value, true);

      return result;
    }
    catch (Exception)
    {
      return SecurityLevel.Disabled;
    }
  }

  public static MesReportCsvStyle? ToReportCsvStyle(this XAttribute attribute)
  {
    if (attribute == null) return null;

    try
    {
      var result = (MesReportCsvStyle)Enum.Parse(typeof(MesReportCsvStyle), attribute.Value, true);

      return result;
    }
    catch (Exception ex)
    {
      return null;
    }
  }

  public static MesIFramePosition ToIFramePosition(this XAttribute attribute)
  {
    if (attribute == null
      || string.IsNullOrWhiteSpace(attribute.Value)) return MesIFramePosition.After;

    try
    {
      var iFramePosition = (MesIFramePosition)Enum.Parse(typeof(MesIFramePosition), attribute.Value, true);

      return iFramePosition;
    }
    catch (Exception)
    {
      return MesIFramePosition.After;
    }
  }

  public static MesReportType? ToReportType(this XAttribute attribute)
  {
    if (attribute == null
      || string.IsNullOrWhiteSpace(attribute.Value)) return null;

    try
    {
    var result = (MesReportType)Enum.Parse(typeof(MesReportType), attribute.Value, true);

    return result;
    }
    catch (Exception)
    {
      return null;
    }
  }

  public static int ToInt(this XAttribute attribute)
  {
    if (attribute == null) return 0;

    if (!int.TryParse(attribute.Value, out int value))
    {
      return 0;
    }

    return value;
  }

  public static bool ToBoolean(this XAttribute attribute)
  {
    if (attribute == null) return false;

    var trueValues = new string[] { "y", "yes", "true" };
    var result = trueValues.Any(x => x == attribute.Value.ToLower());

    return result;
  }

  public static Dictionary<string, string> ToDataDictionary(this XElement element) {
    if (element == null) return new Dictionary<string, string>();

    const string DataPrefix = "Data-";
    var startIndex = DataPrefix.Length;

    var dataDictionary = new Dictionary<string, string>();

    var dataAttributes = element
      .Attributes()
      .Where(at => at.Name.ToString().StartsWith(DataPrefix))
      .ToList();

    dataAttributes.ForEach(at =>
    {
      var key = at.Name.ToString().Substring(startIndex);
      var value = at.Value;

      dataDictionary.Add(key, value);
    });

    return dataDictionary;
  }
}