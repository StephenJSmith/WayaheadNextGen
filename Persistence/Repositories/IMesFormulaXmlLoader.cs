using Domain.Entities;

namespace Persistence.Repositories;

public interface IMesFormulaXmlLoader
{
  MesFormula GetFormulaStepsWithEventSubStep(
    string formulaPathFile, string formulaName, int edition, int revision);

  MesFormula GetFormulaSteps(string formulaPathFile,
  string formulaName, int edition, int revision);
}