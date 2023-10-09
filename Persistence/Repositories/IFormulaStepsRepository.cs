using Domain.Entities;

namespace Persistence.Repositories;

public interface IFormulaStepsRepository
{
  Task<MesFormula> GetFormulaStepsWithEventSubStepAsync(
    string formulaName, int edition, int revision, string formulaPathFile);

  MesFormula GetFormulaStepsWithEventSubStep(
    string formulaName, int edition, int revision, string formulaPathFile);
}