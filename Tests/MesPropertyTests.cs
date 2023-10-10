using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class MesPropertyTests {
  private const string TestPathFile = "901020.xml";
  private const string TestFormulaName = "901020";
  private const int TestEdition = 1;
  private const int TestRevision = 1;
 
  private MesProperty GetSubjectUnderTest(int testOperationNumber, 
    int testPhaseNumber, int testStepNumber, int testSubStepNumber, string testPropertyName)
  {
    var loader = new MesFormulaXmlLoader();
    var mesFormula = loader.GetFormulaStepsWithEventSubStep(TestFormulaName, TestEdition, TestRevision, TestPathFile);
    var editKeys = GetEditKeysForProperty(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber, testPropertyName);
    var sut = mesFormula.GetMesProperty(editKeys);

    return sut;
  }

  private MesFormulaEditKeys GetEditKeysForProperty(
    int operationNumber, int phaseNumber, int stepNumber, 
    int subStepNumber, string propertyName)
  {
    return new MesFormulaEditKeys {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = operationNumber,
      PhaseNumber = phaseNumber,
      StepNumber = stepNumber,
      SubStepNumber = subStepNumber,
      PropertyName = propertyName
    };
  }

  [Theory]
  [InlineData(1, 1, 1, 0, "$Event_1.1.1", true)]
  [InlineData(1, 1, 1, 1, "RCV_Instructions", false)]
  [InlineData(5, 1, 5, 0, "$Event_5.1.5", true)]
  [InlineData(5, 1, 5, 1,  "MIX_AckTransfer", false)]
  [InlineData(8, 1, 1, 1, "BCO_MesEvent", true)]
  [InlineData(9, 1, 1, 1, "QAC_MesEvent", true)]
  public void IsMesEvent(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, string testPropertyName, bool expected) {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber, testPropertyName);

    var actual = sut.IsMesEvent;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1, 0, "$Event_1.1.1", true)]
  [InlineData(1, 1, 1, 1, "RCV_Instructions", false)]
  [InlineData(5, 1, 5, 0, "$Event_5.1.5", true)]
  [InlineData(5, 1, 5, 1,  "MIX_AckTransfer", false)]
  [InlineData(8, 1, 1, 1, "BCO_MesEvent", false)]
  [InlineData(9, 1, 1, 1, "QAC_MesEvent", false)]
  public void IsInsertedMesEvent(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, string testPropertyName, bool expected) {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber, testPropertyName);

    var actual = sut.IsInsertedMesEvent;

    actual.Should().Be(expected);
  }
}