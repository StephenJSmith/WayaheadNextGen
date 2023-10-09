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
    string formulaName, int edition, int revision, string formulaPathFile)
  {
    return _mesFormulaXmlLoader.GetFormulaStepsWithEventSubStep(
      formulaName, edition, revision, formulaPathFile);
  }

    public Task<MesFormula> GetFormulaStepsWithEventSubStepAsync(
    string formulaName, int edition, int revision, string formulaPathFile
  ) {
    return Task.Run(() => GetFormulaStepsWithEventSubStep(
      formulaName, edition, revision, formulaPathFile));
  }

    public Task<MesFormula> GetFormulaStepsWithEventSubStepAsync(string formulaPathFile, string formulaName, int edition, int revision)
    {
        throw new NotImplementedException();
    }

}