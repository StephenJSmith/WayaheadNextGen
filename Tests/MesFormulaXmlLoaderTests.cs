using Persistence.Repositories;

namespace Tests;

public class MesFormulaXmlLoaderTests
{
  [Fact]
  public void GetFormulaStepsWithEventSubStep()
  {
    var testPathFile = "901020.xml";
    var testFormulaName = "901020";
    var testEdition = 1;
    var testRevision = 1;
    var sut = new MesFormulaXmlLoader();

    var actual = sut.GetFormulaStepsWithEventSubStep(testPathFile, testFormulaName, testEdition, testRevision);
  }

  [Fact]
  public void GetFormulaSteps()
  {
    var testPathFile = "901020.xml";
    var testFormulaName = "901020";
    var testEdition = 1;
    var testRevision = 1;
    var sut = new MesFormulaXmlLoader();

    var actual = sut.GetFormulaSteps(testPathFile, testFormulaName, testEdition, testRevision);
  }
}

