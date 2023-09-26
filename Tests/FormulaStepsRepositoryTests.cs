using Persistence.Repositories;

namespace Tests
{
  public class FormulaStepsRepositoryTests
  {
    [Fact]
    public void GetFormulaSteps()
    {
      //var testPathFile = @"D:\CSource\Wayahead.Web.Mes\3WSApp\3WS.Web\App_Sites\MyDev\MesFormula\10-Standard\901020\1.1\901020.xml";
      var testPathFile = "901020.xml";
      var testFormulaName = "901020";
      var testEdition = 1;
      var testRevision = 1;
      var sut = new FormulaStepsRepository();

      var actual = sut.GetFormulaSteps(testPathFile, testFormulaName, testEdition, testRevision);

    }

    [Fact]
    public void GetFormulaStepsWithEventSubStep()
    {
      var testPathFile = "901020.xml";
      var testFormulaName = "901020";
      var testEdition = 1;
      var testRevision = 1;
      var sut = new FormulaStepsRepository();

      var actual = sut.GetFormulaStepsWithEventSubStep(testPathFile, testFormulaName, testEdition, testRevision);
    }
  }
}