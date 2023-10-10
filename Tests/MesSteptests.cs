using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class MesStepTests 
{
  private const string TestPathFile = "901020.xml";
  private const string TestFormulaName = "901020";
  private const int TestEdition = 1;
  private const int TestRevision = 1;
  private const string InsertedMesEventDescription =  "Event entry - Please enter any Events / Deviations / Comments in the table below.";

  private MesStep GetSubjectUnderTest(int testOperationNumber, 
    int testPhaseNumber, int testStepNumber)
  {
    var loader = new MesFormulaXmlLoader();
    var mesFormula = loader.GetFormulaStepsWithEventSubStep(TestFormulaName, TestEdition, TestRevision, TestPathFile);
    var editKeys = GetEditKeysForStep(testOperationNumber, testPhaseNumber, testStepNumber);
    var sut = mesFormula.GetMesStep(editKeys);

    return sut;
  }

  private MesFormulaEditKeys GetEditKeysForStep(
    int operationNumber, int phaseNumber, int stepNumber)
  {
    return new MesFormulaEditKeys {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = operationNumber,
      PhaseNumber = phaseNumber,
      StepNumber = stepNumber
    };
  }

  private MesFormulaEditKeys GetEditKeysForSubStep(
    int operationNumber, int phaseNumber, int stepNumber, int subStepNumber)
  {
    return new MesFormulaEditKeys {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = operationNumber,
      PhaseNumber = phaseNumber,
      StepNumber = stepNumber,
      SubStepNumber = subStepNumber
    };
  }

  [Theory]
  [InlineData(1, 1, 1, "1.1.1")]
  [InlineData(5, 1, 4, "5.1.4")]
  [InlineData(8, 2, 1, "8.2.1")]
  public void StepHierarchicalNumber(int testOperationNumber, int testPhaseNumber, int testStepNumber, string expected) {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber);

    var actual = sut.StepHierarchicalNumber;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1, true)]
  [InlineData(5, 1, 4, true)]
  [InlineData(8, 1, 1, false)]
  [InlineData(8, 2, 1, true)]
  [InlineData(9, 1, 1, false)]
  public void HasInsertedMesEventSubStep(int testOperationNumber, int testPhaseNumber, int testStepNumber, bool expected) {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber);

    var actual = sut.HasInsertedMesEventSubStep;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1, 0)]
  [InlineData(5, 1, 4, 0)]
  [InlineData(8, 1, 1, -1)]
  [InlineData(8, 2, 1, 0)]
  [InlineData(9, 1, 1, -1)]
  public void MesEventSubStepNumber(int testOperationNumber, int testPhaseNumber, int testStepNumber, int expected) {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber);

    var actual = sut.MesEventSubStepNumber;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1, 1, "Picking Instructions")]
  [InlineData(1, 1, 1, 0, InsertedMesEventDescription)]
  [InlineData(1, 1, 2, 1, "Confirm Batch Details")]
  [InlineData(1, 1, 2, 0, InsertedMesEventDescription)]
  [InlineData(4, 1, 2, 3, "Confirm Batch Details")]
  [InlineData(4, 1, 2, 0, InsertedMesEventDescription)]
  public void GetMesSubStep(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, string expectedSubStepDescription) { 
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber);
    var testEditKeys = GetEditKeysForSubStep(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);

    var actual = sut.GetMesSubStep(testEditKeys);

    actual.Description1.Should().Be(expectedSubStepDescription);
  }
}