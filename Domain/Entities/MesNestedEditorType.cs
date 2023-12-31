namespace Domain.Entities;

public class MesNestedEditorType {
  public int Number { get; set; }
  public string Name { get; set; }
  public string Description1 { get; set; }
  public string Description2 { get; set; }
  public MesFormulaEditKeys ParentEditKeys { get; set; }
  public List<MesProperty> Properties { get; set; }
}