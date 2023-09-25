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
  public int RichEditorRows { get; set; }
  public int RichEditorFontSize { get; set; }
  public string ReviewESignatureESignReasons { get; set; }
  public bool DispensingShowBatchItemOnly { get; set; }
  public string DispensingUrl { get; set; }
}