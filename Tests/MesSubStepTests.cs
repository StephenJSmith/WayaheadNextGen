using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class MesSubStepTests {
  private const string TestPathFile = "901020.xml";
  private const string TestFormulaName = "901020";
  private const int TestEdition = 1;
  private const int TestRevision = 1;
  private const string InsertedMesEventDescription =  "Event entry - Please enter any Events / Deviations / Comments in the table below.";

  private MesSubStep GetSubjectUnderTest(int testOperationNumber, 
    int testPhaseNumber, int testStepNumber, int testSubStepNumber)
  {
    var loader = new MesFormulaXmlLoader();
    var mesFormula = loader.GetFormulaStepsWithEventSubStep(TestFormulaName, TestEdition, TestRevision, TestPathFile);
    var editKeys = GetEditKeysForSubStep(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);
    var sut = mesFormula.GetMesSubStep(editKeys);

    return sut;
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
  [InlineData(1, 1, 1, 1, "1.1.1.1")]
  [InlineData(1, 1, 1, 0, "1.1.1.0")]
  [InlineData(1, 1, 2, 1, "1.1.2.1")]
  [InlineData(4, 1, 2, 0, "4.1.2.0")]
  [InlineData(4, 1, 2, 3, "4.1.2.3")]
  public void SubStepHierarchicalNumber(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, string expected) {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);

    var actual = sut.SubStepHierarchicalNumber;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1, 1, "RCV_Instructions", "Picking Instructions", "RichEditor")]
  [InlineData(1, 1, 1, 0, "$Event_1.1.1", "", "YesNo")]
  [InlineData(7, 1, 2, 2, "TST_AverageRelativeDensity","Average Relative Density", "Decimal")]
  public void FirstProperty(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, string expectedName, string expectedDescription, string expectedEditorType) {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);

    var actual = sut.FirstProperty;

    actual.Name.Should().Be(expectedName);
    actual.Description1.Should().Be(expectedDescription);
    actual.EditorType.ToString().Should().Be(expectedEditorType);
  }

  [Theory]
  [InlineData(1, 1, 1, 0, true)]
  [InlineData(1, 1, 1, 1, false)]
  [InlineData(5, 1, 5, 0, true)]
  [InlineData(5, 1, 5, 1, false)]
  [InlineData(8, 1, 1, 1, true)]
  [InlineData(9, 1, 1, 1, true)]
  public void IsMesEvent(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, bool expected) { 
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);

    var actual = sut.IsMesEvent;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1, 0, true)]
  [InlineData(1, 1, 1, 1, false)]
  [InlineData(5, 1, 5, 0, true)]
  [InlineData(5, 1, 5, 1, false)]
  [InlineData(8, 1, 1, 1, false)]
  [InlineData(9, 1, 1, 1, false)]
  public void IsInsertedMesEvent(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, bool expected) { 
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);

    var actual = sut.IsInsertedMesEvent;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1, 0, "$Event_1.1.1")]
  [InlineData(5, 1, 5, 0, "$Event_5.1.5")]
  public void InsertedMesEventSubStepDescriptionAndPropertyName(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, string expectedPropertyName) { 
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);
    var expectedDescription = InsertedMesEventDescription;

    var actualDescription = sut.Description1;
    var actualPropertyName = sut.FirstProperty.Name;

    actualDescription.Should().Be(expectedDescription);
    actualPropertyName.Should().Be(expectedPropertyName);
  }

  [Theory]
  [InlineData(1, 1, 1, 1, "RCV_Instructions", "Picking Instructions", "RichEditor")]
  [InlineData(1, 1, 1, 0, "$Event_1.1.1", "", "YesNo")]
  [InlineData(7, 1, 2, 2, "TST_TotalNettWeight","Total Nett Weight", "Decimal")]
  public void GetMesProperty(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, string testPropertyName, string expectedDescription, string expectedEditorType) {
    var sut = GetSubjectUnderTest(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);
    var testEditKeys = GetEditKeysForProperty(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber, testPropertyName);

    var actual = sut.GetMesProperty(testEditKeys);

    actual.Description1.Should().Be(expectedDescription);
    actual.EditorType.ToString().Should().Be(expectedEditorType);
  }
}