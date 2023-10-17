using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class MesPhaseTests
{
  private const string TestPathFile = "901020.xml";
  private const string TestFormulaName = "901020";
  private const int TestEdition = 1;
  private const int TestRevision = 1;

  private MesPhase GetSubjectUnderTest(int testOperationNumber, int testPhaseNumber)
  {
    var loader = new MesFormulaXmlLoader();
    var mesFormula = loader.GetFormulaStepsWithEventSubStep(TestFormulaName, TestEdition, TestRevision, TestPathFile);
    var editKeys = GetEditKeysForPhase(testOperationNumber, testPhaseNumber);
    var sut = mesFormula.GetMesPhase(editKeys);

    return sut;
  }

  private MesFormulaEditKeys GetEditKeysForPhase(
    int operationNumber, int phaseNumber)
  {
    return new MesFormulaEditKeys
    {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = operationNumber,
      PhaseNumber = phaseNumber
    };
  }

  private MesFormulaEditKeys GetEditKeysForStep(
    int operationNumber, int phaseNumber, int stepNumber)
  {
    return new MesFormulaEditKeys
    {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = operationNumber,
      PhaseNumber = phaseNumber,
      StepNumber = stepNumber
    };
  }

  [Theory]
  [InlineData(1, 1, "1.1")]
  [InlineData(7, 2, "7.2")]
  public void PhaseHierarchicalNumber(int testOperationNumber, int testPhaseNumber, string expected)
  {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber);

    var actual = sut.PhaseHierarchicalNumber;

    actual.Should().Be(expected);
  }

  [Fact]
  public void PhaseHierarchicalNumber_WhenNoParentEditKeys_UseQuestionMarkForParentProperties()
  {
    var testOperationNumber = 7;
    var testPhaseNumber = 2;
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber);
    sut.ParentEditKeys = null;
    var expected = "?.2";

    var actual = sut.PhaseHierarchicalNumber;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1, "Picking Instructions")]
  [InlineData(7, 2, 3, "Packing")]
  public void GetMesStep(int testOperationNumber, int testPhaseNumber, int testStepNumber, string expectedStepName)
  {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber);
    var testEditKeys = GetEditKeysForStep(testOperationNumber, testPhaseNumber, testStepNumber);

    var actual = sut.GetMesStep(testEditKeys);

    actual.Description1.Should().Be(expectedStepName);
  }

  [Fact]
  public void GetMesStep_ThrowsException_WhenStepNotFound()
  {
    var testOperationNumber = 1;
    var testPhaseNumber = 1;
    var testStepNumber = 3;
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber);
    var testEditKeys = GetEditKeysForStep(testOperationNumber, testPhaseNumber, testStepNumber);

    Assert.Throws<ProgramException>(() => sut.GetMesStep(testEditKeys));
  }

  [Theory]
  [InlineData(1, 1, 1, new int[] { 1 })]
  [InlineData(7, 2, 3, new int[] { 1, 2, 3 })]
  public void GetMesStepAndPreviousSteps(int testOperationNumber, int testPhaseNumber, int testStepNumber, int[] expectedStepNumbers)
  {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber);
    var testEditKeys = GetEditKeysForStep(testOperationNumber, testPhaseNumber, testStepNumber);

    var actual = sut.GetMesStepAndPreviousSteps(testEditKeys);

    actual.Count.Should().Be(expectedStepNumbers.Length);
    actual
      .Select(st => st.Number)
      .ToArray()
      .Should().BeEquivalentTo(expectedStepNumbers);
  }

  [Theory]
  [InlineData(1, 1, "Picking Instructions")]
  [InlineData(7, 2, "Packing Instructions")]
  public void GetFirstStep(int testOperationNumber, int testPhaseNumber, string expectedStepDescription)
  {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber);

    var actual = sut.GetFirstStep();

    actual.Description1.Should().Be(expectedStepDescription);
  }
}