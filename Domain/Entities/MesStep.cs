using Domain.Enums;

namespace Domain.Entities;

public class MesStep {
  public int Number { get; set; }
	public int OperationNumber { get; set; }
	public int PhaseNumber { get; set; }
	public string OperationDescription1 { get; set; }
	public string OperationDescription2 { get; set; }
	public string PhaseDescription1 { get; set; }
	public string PhaseDescription2 { get; set; }
	public string HierarchicalNumber { get; set; }
	public MesFormulaEditKeys ParentEditKeys { get; set; }
	public string Name { get; set; }
	public string Description1 { get; set; }
	public string Description2 { get; set; }
	public string CopyCommand { get; set; }
  public List<MesSubStep> SubSteps { get; set; }
	public string MenuDescription { get; set; }
	public string NextStepAllowed { get; set; }
	public bool EnableMultiRun { get; set; }
  public string NewRunLinkedSteps { get; set; }
  public bool AlwaysEnableNewRun { get; set; }
	public SecurityLevel SecurityLevel { get; set; }
	public string PreRenderScript { get; set; }
	public string PostExecutionScript { get; set; }
	public string ReferenceNo { get; set; }
}