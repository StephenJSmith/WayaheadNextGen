using Domain.Entities;
using Persistence.Repositories;

namespace Tests
{
  public class FormulaStepsRepositoryTests
  {
    private const string TestPathFile =  "901020.xml";
    private const string TestFormulaName = "901020";
    private const int TestEdition = 1;
    private const int TestRevision = 1;

    [Fact]
    public void GetFormulaStepsWithEventSubStep()
    {
      var mesFormulaXmlLoader = new MesFormulaXmlLoader();
      var sut = new FormulaStepsRepository(mesFormulaXmlLoader);

      var actual = sut.GetFormulaStepsWithEventSubStep(TestFormulaName, TestEdition, TestRevision, TestPathFile);
    }

    [Fact]
    public async Task GetFormulaStepsWithEventSubStepAsync()
    {
      var mesFormulaXmlLoader = new MesFormulaXmlLoader();
      var sut = new FormulaStepsRepository(mesFormulaXmlLoader);

      var actual = await sut.GetFormulaStepsWithEventSubStepAsync(TestFormulaName, TestEdition, TestRevision, TestPathFile);
    }
  }
}