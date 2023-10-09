using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class MesFormulaTests {
  private const string TestPathFile = "901020.xml";
  private const string TestFormulaName = "901020";
  private const int TestEdition = 1;
  private const int TestRevision = 1;
  private const string InsertedMesEventDescription =  "Event entry - Please enter any Events / Deviations / Comments in the table below.";

  private MesFormula GetSubjectUnderTest()
  {
    var loader = new MesFormulaXmlLoader();
    var sut = loader.GetFormulaStepsWithEventSubStep(TestFormulaName, TestEdition, TestRevision, TestPathFile);

    return sut;
  }

  private MesFormulaEditKeys GetEditKeys(int operationNumber)
  {
    return new MesFormulaEditKeys {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = operationNumber
    };
  }

  private MesFormulaEditKeys GetEditKeys(
    int operationNumber, int phaseNumber)
  {
    return new MesFormulaEditKeys {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = operationNumber,
      PhaseNumber = phaseNumber
    };
  }

  private MesFormulaEditKeys GetEditKeys(
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

  private MesFormulaEditKeys GetEditKeys(
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

  private MesFormulaEditKeys GetEditKeys(
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

  private MesFormulaEditKeys GetEditKeysForNestedEditorType(int nestedEditorTypeNumber)
  {
    return new MesFormulaEditKeys {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      NestedEditorTypeNumber = nestedEditorTypeNumber
    };
  }

  [Theory]
  [InlineData(1, true)]
  [InlineData(9, true)]
  [InlineData(10, false)]
  public void HasOperation_ReturnsExpectedResult(int operationNumber, bool expected)
  {
    var sut = GetSubjectUnderTest();

    var actual = sut.HasOperation(operationNumber);

    actual.Should().Be(expected);
  } 

  [Theory]
  [InlineData(1, "Picking", new string[]{"Picking"})]
  [InlineData(2, "Check Order", new string[] {"Check Order"} )]
  [InlineData(3, "Dispensing", new string[] {"Dispensing" })]
  [InlineData(8, "Batch Completion", new string[] {"Events/Comments Entry", "Production Batch Close" })]
  public void GetMesOperation_ReturnsOperation_WhenExistingOperation(
    int testOperationNumber, string exepectedDescription1, string[] expectedPhaseDescriptions
  )
  {
    var sut = GetSubjectUnderTest();
    var testEditKeys = GetEditKeys(testOperationNumber);

    var actual = sut.GetMesOperation(testEditKeys);

    actual.Description1.Should().Be(exepectedDescription1);
    actual.Phases
      .Select(ph => ph.Description1)
      .ToArray()
      .Should().BeEquivalentTo(expectedPhaseDescriptions);
  }

  [Fact]
  public void GetMesOperation_ThrowsExpectedException_WhenNoOperationInMesFormulaEditKeys()
  {
    var sut = GetSubjectUnderTest();
    var testOperationNumber = 10;
    var testEditKeys = GetEditKeys(testOperationNumber);

    Assert.Throws<ProgramException>(() => sut.GetMesOperation(testEditKeys));
  }

  [Fact]
  public void GetMesOperation_ReturnNull_WhenNoOperationNumber()
  {
    var sut = GetSubjectUnderTest();
    var testOperationNumber = 10;

    var actual = sut.GetMesOperation(testOperationNumber);

    actual.Should().BeNull();
  }

  [Theory]
  [InlineData(1, true)]
  [InlineData(2, false)]
  public void IsFirstOperation_ExpectedBooleanValue(int testOperationNumber, bool expected)
  {
    var sut = GetSubjectUnderTest();

    var actual = sut.IsFirstOperation(testOperationNumber);

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, "Picking", new string[] {"Picking Instructions", "Print Picking Report"})]
  [InlineData(7, 2, "Pack", new string[] {"Packing Instructions",  "Packing Equipment", "Packing"})]
  public void GetMesPhase(int testOperationNumber, int testPhaseNumber, string expectedPhaseDescription, string[] expectedStepDescriptions)
  {
    var sut = GetSubjectUnderTest();
    var testEditKeys = GetEditKeys(testOperationNumber, testPhaseNumber);

    var actual = sut.GetMesPhase(testEditKeys);

    actual.Description1.Should().Be(expectedPhaseDescription);
    actual.Steps
      .Select(st => st.Description1)
      .ToArray()
      .Should().BeEquivalentTo(expectedStepDescriptions);
  }

  [Theory]
  [InlineData(1, 1, true)]
  [InlineData(1, 2, false)]
  [InlineData(8, 1, true)]
  [InlineData(8, 2, false)]
  public void IsFirstPhase(int testOperationNumber, int testPhaseNumber, bool expected)
  {
    var sut= GetSubjectUnderTest();

    var actual = sut.IsFirstPhase(testOperationNumber, testPhaseNumber);

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(1, 1, 1,  "Picking Instructions")]
  [InlineData(7, 2, 3,  "Packing")]
  public void GetMesStep(int testOperationNumber, int testPhaseNumber, int testStepNumber, string expectedStepDescription) 
  {
    var sut = GetSubjectUnderTest();
    var testEditKeys = GetEditKeys(testOperationNumber, testPhaseNumber, testStepNumber);

    var actual = sut.GetMesStep(testEditKeys);

    actual.Description1.Should().Be(expectedStepDescription);
  }

  [Theory]
  [InlineData(1, 1, 1, new string[]{"Picking Instructions"})]
  [InlineData(7, 2, 3, new string[] {"Packing Instructions", "Packing Equipment", "Packing"})]
  public void GetMesStepAndPreviousSteps(int testOperationNumber, int testPhaseNumber, int testStepNumber, string[] expectedStepDescriptions)
  {
    var sut = GetSubjectUnderTest();
    var testEditKeys = GetEditKeys(testOperationNumber, testPhaseNumber, testStepNumber);

    var actual = sut.GetMesStepAndPreviousSteps(testEditKeys);

    actual.Count.Should().Be(expectedStepDescriptions.Length);
    actual
      .Select(st => st.Description1)
      .ToArray()
      .Should().BeEquivalentTo(expectedStepDescriptions);
  }

  [Theory]
  [InlineData(1, 1, "Picking Instructions")]
  [InlineData(7, 2, "Packing Instructions")]
  public void GetFirstStep(int testOperationNumber, int testPhaseNumber, string expectedDescription)
  {
    var sut = GetSubjectUnderTest();
    var testEditKeys = GetEditKeys(testOperationNumber, testPhaseNumber);

    var actual  = sut.GetFirstStep(testEditKeys);

    actual.Description1.Should().Be(expectedDescription);
  }

  [Theory]
  [InlineData(1, 1, 1, 1, "Picking Instructions")]
  [InlineData(1, 1, 1, 0, InsertedMesEventDescription)]
  [InlineData(1, 1, 2, 1, "Confirm Batch Details")]
  [InlineData(1, 1, 2, 0, InsertedMesEventDescription)]
  [InlineData(4, 1, 2, 3, "Confirm Batch Details")]
  [InlineData(4, 1, 2, 0, InsertedMesEventDescription)]
  public void GetMesSubStep(int testOperationNumber, int testPhaseNumber, int testStepNumber, int testSubStepNumber, string expectedDescription)
  {
    var sut= GetSubjectUnderTest();
    var testEditKeys = GetEditKeys(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber);

    var actual = sut.GetMesSubStep(testEditKeys);

    actual.Description1.Should().Be(expectedDescription);
  }

  [Theory]
  [InlineData(1, 1, 1, 1, "RCV_Instructions", "Picking Instructions", "RichEditor")]
  [InlineData(1, 1, 1, 0, "$Event_1.1.1", "", "YesNo")]
  [InlineData(7, 1, 2, 2, "TST_TotalNettWeight","Total Nett Weight", "Decimal")]
  public void GetMesProperty(int testOperationNumber, int testPhaseNumber, 
    int testStepNumber, int testSubStepNumber, 
    string testPropertyName, string expectedDescription, string expectedEditorType)
  {
    var sut= GetSubjectUnderTest();
    var testEditKeys = GetEditKeys(testOperationNumber, testPhaseNumber, testStepNumber, testSubStepNumber, testPropertyName);

    var actual = sut.GetMesProperty(testEditKeys);

    actual.Description1.Should().Be(expectedDescription);
    actual.EditorType.ToString().Should().Be(expectedEditorType);
  }

  [Fact]
  public void GetMesNestedEditorType_ReturnsNestedEditorType_WhenEditKeysContainsNumber()
  {
    var sut = GetSubjectUnderTest();
    var testEditKeys = GetEditKeysForNestedEditorType(1);
    var expectedName = "TST_MedicineTest";
    var expectedPropertyNames = new List<string> { "BinNumber", "TareWeight", "GrossWeight", "NettWeight", "RelativeDensity", "CheckSmell", "CheckColor", "CheckImpurity"};

    var actual = sut.GetMesNestedEditorType(testEditKeys);

    actual.Name.Should().Be(expectedName);
    actual.Properties
      .Select(pr => pr.Name)
      .ToArray()
      .Should().BeEquivalentTo(expectedPropertyNames);
  }

  [Fact]
  public void GetMesNestedEditorType_ThrowsException_WhenEditorKeysNotContainsNestedEditorTypeNumber()
  {
    var sut = GetSubjectUnderTest();
    var testEditKeys = GetEditKeysForNestedEditorType(2);

    Assert.Throws<ProgramException>(() => sut.GetMesOperation(testEditKeys));
  }
}