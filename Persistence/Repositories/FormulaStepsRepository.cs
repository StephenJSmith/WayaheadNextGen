using Domain.Entities;

namespace Persistence.Repositories;

public class FormulaStepsRepository
{
  private readonly IMesFormulaXmlLoader _mesFormulaXmlLoader;

  public FormulaStepsRepository(IMesFormulaXmlLoader mesFormulaXmlLoader)
  {
    _mesFormulaXmlLoader = mesFormulaXmlLoader;
  }

  public MesFormula GetFormulaStepsWithEventSubStep(
    string formulaPathFile, string formulaName, int edition, int revision)
  {
    return _mesFormulaXmlLoader.GetFormulaStepsWithEventSubStep(
      formulaPathFile, formulaName, edition, revision);
  }

  public MesFormula GetFormulaSteps(string formulaPathFile,
  string formulaName, int edition, int revision)
  {
    return _mesFormulaXmlLoader.GetFormulaSteps(formulaPathFile, formulaName, edition, revision);
  }
}