using Domain.Entities;

namespace Persistence.Repositories;

public class FormulaStepsRepository : IFormulaStepsRepository
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

  public Task<MesFormula> GetFormulaStepsWithEventSubStepAsync(
    string formulaPathFile, string formulaName, int edition, int revision
  ) {
    return Task.Run(() => GetFormulaStepsWithEventSubStep(
      formulaPathFile, formulaName, edition, revision));
  }

  public MesFormula GetFormulaSteps(string formulaPathFile,
  string formulaName, int edition, int revision)
  {
    return _mesFormulaXmlLoader.GetFormulaSteps(
      formulaPathFile, formulaName, edition, revision);
  }

  public Task<MesFormula> GetFormulaStepsAsync(string formulaPathFile,
  string formulaName, int edition, int revision) {
    return Task.Run(() => GetFormulaSteps(
      formulaPathFile, formulaName, edition, revision));
  }
}