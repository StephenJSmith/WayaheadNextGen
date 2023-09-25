namespace Domain.Entities;

public class MesPhase {
  public int Number { get; set; }
	public string HierarchicalNumber { get; set; }
	public string Name { get; set; }
	public string Description1 { get; set; }
	public string Description2 { get; set; }
	public string CopyCommand { get; set; }
  public List<MesStep> Steps { get; set; }
	public string MenuDescription { get; set; }
	public string NextPhaseAllowed { get; set; }
	public bool DisableFlowControl { get; set; }
	public bool EnableMultiRun { get; set; }
	public string PreRenderScript { get; set; }
	public string PostExecutionScript { get; set; }
	public string ReferenceNo { get; set; }

}