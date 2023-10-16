namespace Domain.Entities;

public class MesActionKeys
{
  public string Batch { get; set; }
  public string LinkedBatch { get; set; }
  public string Formula { get; set; }
  public int Edition { get; set; }
  public int Revision { get; set; }
  public string FormulaName => $"{Formula}/{Edition}.{Revision}";
  public int RunNumber { get; set; }
  public int MaxRunNumber { get; set; }
  public string Category { get; set; }
  public int OperationNumber { get; set; }
  public int PhaseNumber { get; set; }
  public int StepNumber { get; set; }
  public int SubStepNumber { get; set; }
  public int PropertyNumber { get; set; }
  public string PropertyName { get; set; }
  public string NestedPropertyNumber { get; set; }
  public string NestedPropertyName { get; set; }

  public string PhaseHierarchicalNumber => $"{OperationNumber}.{PhaseNumber}";

  public string StepHierarchicalNumber => $"{OperationNumber}.{PhaseNumber}.{StepNumber}";

  public string SubStepHierarchicalNumber => $"{OperationNumber}.{PhaseNumber}.{StepNumber}.{SubStepNumber}";

  public bool IsInitialMesPageLoad => string.IsNullOrWhiteSpace(Batch);

}