namespace Domain.Entities;

public class MesFormulaEditKeys {
  public string Formula { get; set; }
  public int Edition { get; set; }
  public int Revision { get; set; }
  public string FormulaName => $"{Formula}/{Edition}.{Revision}";
  public int OperationNumber { get; set; }
  public int PhaseNumber { get; set; }
  public int StepNumber { get; set; }
  public int SubStepNumber { get; set; }
  public int PropertyNumber { get; set; }
  public string PropertyName { get; set; }
  public string NestedPropertyNumber { get; set; }
  public string NestedPropertyName { get; set; }
  public int NestedEditorTypeNumber { get; set; }
  public string NestedEditorTypeName { get; set; }
}