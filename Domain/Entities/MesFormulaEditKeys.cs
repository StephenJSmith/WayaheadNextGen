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
  public string PhaseHierarchicalNumber => $"{OperationNumber}.{PhaseNumber}";
  public string StepHierarchicalNumber => $"{OperationNumber}.{PhaseNumber}.{StepNumber}";
  public string SubStepHierarchicalNumber => $"{OperationNumber}.{PhaseNumber}.{StepNumber}.{SubStepNumber}";

  public bool CanSearchProperty => string.IsNullOrWhiteSpace(NestedPropertyName);
  public bool CanSearchNestedPropertyChild => !CanSearchProperty && NestedPropertyName != PropertyName;

  public MesFormulaEditKeys () {}

  public MesFormulaEditKeys(MesActionKeys actionKeys)
  {
    Formula = actionKeys.Formula;
    Edition = actionKeys.Edition;
    Revision = actionKeys.Revision;
    OperationNumber = actionKeys.OperationNumber;
    PhaseNumber = actionKeys.PhaseNumber;
    StepNumber = actionKeys.StepNumber;
    SubStepNumber = actionKeys.SubStepNumber;
    PropertyNumber = actionKeys.PropertyNumber;
    PropertyName = actionKeys.PropertyName;
    NestedPropertyNumber = actionKeys.NestedPropertyNumber;
    NestedPropertyName = actionKeys.NestedPropertyName;
  }
  public MesFormulaEditKeys Clone()
  {
      return (MesFormulaEditKeys)this.MemberwiseClone();
  }
}
