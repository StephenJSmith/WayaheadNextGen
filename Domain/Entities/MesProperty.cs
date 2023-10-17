using Domain.Enums;

namespace Domain.Entities;

public class MesProperty
{
  public const string InsertedMesEventPropertyNamePrefix = "$Event_";
  public const int InsertedMesEventPropertyNumber = 0;

  public string Name { get; set; }
  public string NestedEditorName { get; set; }
  public int PropertyNumber { get; set; }
  public string Description1 { get; set; }
  public string Description2 { get; set; }
  public string CopyCommand { get; set; }
  public string Hint { get; set; }
  public string AutoCalculation { get; set; }
  public string Validation { get; set; }
  public int RepeatMinTimes { get; set; }
  public int RepeatMaxTimes { get; set; }
  public TimeSpan RepeatInterval { get; set; }
  public string ValueRange { get; set; }
  public SecurityLevel SecurityLevel { get; set; }

  public bool ESignCommentsRequired { get; set; }
  public string PreRenderScript { get; set; }
  public string PostExecutionScript { get; set; }
  public string ReferenceNo { get; set; }
  public bool CheckCompletion { get; set; }
  public string CompletionErrorMessage { get; set; }
  public string PostDeletionScript { get; set; }
  public MesEditorType EditorType { get; set; }
  public string DefaultValue { get; set; }
  public bool Pivot { get; set; }
  public string Uom { get; set; }
  public string RunProgram1 { get; set; }
  public string RunProgram2 { get; set; }
  public string IFrameUrl { get; set; }
  public string IFrameUrl2 { get; set; }
  public MesIFramePosition IFramePosition { get; set; }
  public string ChildReport { get; set; }
  public bool FullSize { get; set; }
  public bool PictureEvidence { get; set; }
  public bool Disable { get; set; }
  public bool Hide { get; set; }
  public bool Nullable { get; set; }
  public MesReportType? ReportType { get; set; }
  public int MyProperty { get; set; }
  public MesFormulaEditKeys ParentEditKeys { get; set; }
  public MesReportCsvStyle? ReportCsvStyle { get; set; }
  public int ReportCsvFontSize { get; set; }
  public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
  public bool IsMesEvent => Name != null && Name.Contains("MesEvent", StringComparison.InvariantCultureIgnoreCase)
    || (IFrameUrl != null && IFrameUrl.Contains("MesEvent", StringComparison.InvariantCultureIgnoreCase))
    || (IFrameUrl2 != null && IFrameUrl2.Contains("MesEvent", StringComparison.InvariantCultureIgnoreCase));
  public bool IsInsertedMesEvent => Name != null && Name.ToLower().StartsWith("$event_");

  public bool IsShowAllEvents =>
      ReferenceNo != null
      && ReferenceNo.ToLower().Contains("showallevents=y");

  public static bool IsInsertedMesEventPropertyName(string propertyName)
  {
    return propertyName != null
        && propertyName.StartsWith(InsertedMesEventPropertyNamePrefix, StringComparison.InvariantCultureIgnoreCase);
  }

  public static string GetInsertedMesEventPropertyName(int operationNumber, int phaseNumber, int stepNumber)
  {
    return $"{InsertedMesEventPropertyNamePrefix}{operationNumber}.{phaseNumber}.{stepNumber}";
  }

  public static string GetInsertedMesEventPropertyName(string stepHierarchicalNumber)
  {
    return $"{InsertedMesEventPropertyNamePrefix}{stepHierarchicalNumber}";
  }

  public static (int operationNumber, int phaseNumber, int stepNumber) GetStagesForMesEventPropertyName(string propertyName)
  {
    if (!MesProperty.IsInsertedMesEventPropertyName(propertyName))
    {
      return (0, 0, 0);
    }

    try
    {
      var hierarchicalNumber = propertyName.Substring(InsertedMesEventPropertyNamePrefix.Length);
      var stages = hierarchicalNumber.Split('.');

      return (int.Parse(stages[0]), int.Parse(stages[1]), int.Parse(stages[2]));
    }
    catch (Exception)
    {
      return (0, 0, 0);
    }
  }
}