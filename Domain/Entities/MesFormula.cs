namespace Domain.Entities;

public class MesFormula 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Edition { get; set; }
    public int Revision { get; set; }
    public string Description1 { get; set; }
    public string Description2 { get; set; }
    public string CopyCommand { get; set; }
    public List<MesOperation> Operations { get; set; }
    public List<MesNestedEditorType> NestedEditorTypes { get; set; }
    public DateTime SavedOn { get; set; }
    public string SavedBy { get; set; }
}