using Domain.Entities;

namespace Persistence.Repositories;

public interface IFormulaStepsRepository
{
  Task<MesFormula> GetFormulaStepsWithEventSubStepAsync(
    string formulaPathFile, string formulaName, int edition, int revision);
  Task<MesFormula> GetFormulaStepsAsync(string formulaPathFile,
    string formulaName, int edition, int revision);

  MesFormula GetFormulaStepsWithEventSubStep(
    string formulaPathFile, string formulaName, int edition, int revision);
  MesFormula GetFormulaSteps(string formulaPathFile,
  string formulaName, int edition, int revision);
}