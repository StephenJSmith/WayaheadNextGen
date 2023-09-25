using Domain.Enums;

namespace Domain.Entities;

public class MesProperty {
	public string Name { get; set; }
	public string Description1 { get; set; }
	public string Description2 { get; set; }
	public string CopyCommand { get; set; }
  public string Hint { get; set; }
  public string AutoCalculation { get; set; }
  public string Validation { get; set; }
  public string SecurityLevel { get; set; }

  public bool ESignCommentsRequired { get; set; }
	public string PreRenderScript { get; set; }
	public string PostExecutionScript { get; set; }
	public string ReferenceNo { get; set; }
  public bool CheckCompletion { get; set; }
  public string CompletionErrorMessage { get; set; }
  public string PostDeletionScript { get; set; }
  public MesEditorType EditorType { get; set; }
  public string DefaultValue { get; set; }
  public string Uom { get; set; }
  public string RunProgram1 { get; set; }
  public string RunProgram2 { get; set; }
  public MesIFramePosition IFramePosition { get; set; }
  public string ChildReport { get; set; }
  public bool FullSize { get; set; }
  public bool PictureEvidence { get; set; }
  public bool Disable { get; set; }
  public bool Hide { get; set; }
  public bool Nullable { get; set; }
  public MesReportType ReportType { get; set; }

  // Optional fields per EditorType, RunProgramX, ReportType, ...
  public int Data_RichEditor_Rows { get; set; }
  public int Data_RichEditor_FontSize { get; set; }
  public string Data_ReviewESignature_ESignReasons { get; set; }
  public bool Data_Dispensing_ShowBatchItemOnly { get; set; }
  public string Data_Dispensing_Url { get; set; }
  public string Data_Dashboard_Url { get; set; }
  public string Data_Dashboard_Name { get; set; }
  public string Data_Dashboard_MesView { get; set; }
  public string Data_Dropdown_KeyValues { get; set; }
  public bool Data_YesNo_LimitSelections { get; set; }
  public bool Data_EquipmentIssuing_CheckStartupEquipmentsOnly { get; set; }
  public string Data_EquipmentIssuing_Url { get; set; }
  public string Data_Mfg_Url { get; set; }
  public string Data_Timespan_TimeRange { get; set; }
  public string Data_Timespan_StartEndTimeProperty { get; set; }
  public string Data_Timespan_TimeSpanFormat { get; set; }
  public string Data_Nested_ReportStyle { get; set; }
  public int Data_Nested_ReportFontSize { get; set; }
  public bool Data_Nested_ReportHideCreatedBy { get; set; }
  public bool Data_Nested_ReportHideESignatureBy { get; set; }
  public bool Data_Nested_ReportHideIsDeleted { get; set; }
  public string Data_ManualIPC_Url { get; set; }
  public string Data_MesEvent_Url { get; set; }
}