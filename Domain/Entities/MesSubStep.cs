namespace Domain.Entities;

public class MesSubStep
{
	public int Number { get; set; }
	public string HierarchicalNumber { get; set; }
	public MesFormulaEditKeys ParentEditKeys { get; set; }
	public string Name { get; set; }
	public string Description1 { get; set; }
	public string Description2 { get; set; }
	public string CopyCommand { get; set; }
	public List<MesProperty> Properties { get; set; }
	public string PreRenderScript { get; set; }
	public string PostExecutionScript { get; set; }
	public string ReferenceNo { get; set; }
	public bool IsMesEvent => Properties != null 
		&& Properties.Any(p => p.IsMesEvent);
	public bool IsInsertedMesEvent => Properties != null
		&& Properties.Any(p => p.IsInsertedMesEvent);
}