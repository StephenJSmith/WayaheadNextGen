using Persistence.Repositories;

namespace Tests
{
  public class FormulaStepsRepositoryTests
    {
      [Fact]
      public void LoadXmlFile()
      {
        //var testPathFile = @"D:\CSource\Wayahead.Web.Mes\3WSApp\3WS.Web\App_Sites\MyDev\MesFormula\10-Standard\901020\1.1\901020.xml";
        var testPathFile = "901020.xml";
        var sut = new FormulaStepsRepository();

        var actual = sut.GetFormulaProperties(testPathFile);

      }
    }
}