namespace Domain.Entities;

public class MesNestedEditorType {
  public string Name { get; set; }
  public string Description1 { get; set; }
  public string Description2 { get; set; }
  public List<MesProperty> Properties { get; set; }
}