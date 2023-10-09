using Domain.Entities;

namespace Persistence.Repositories;

public interface IMesFormulaXmlLoader
{
  MesFormula GetFormulaStepsWithEventSubStep(
    string formulaName, int edition, int revision, string formulaPathFile);
}