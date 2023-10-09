using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class MesOperationTests {
  private const string TestPathFile = "901020.xml";
  private const string TestFormulaName = "901020";
  private const int TestEdition = 1;
  private const int TestRevision = 1;

  private MesOperation GetSubjectUnderTest(int testOperationNumber)
  {
    var loader = new MesFormulaXmlLoader();
    var mesFormula = loader.GetFormulaStepsWithEventSubStep(TestFormulaName, TestEdition, TestRevision, TestPathFile);
    var editKeys = GetEditKeysForOperation(testOperationNumber);
    var sut = mesFormula.GetMesOperation(editKeys);

    return sut;
  }

  private MesFormulaEditKeys GetEditKeysForPhase(
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

  private MesFormulaEditKeys GetEditKeysForOperation(int operationNumber)
  {
    return new MesFormulaEditKeys {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = operationNumber
    };
  }

  [Theory]
  [InlineData(1, 1, "Picking", new string[] {"Picking Instructions", "Print Picking Report"})]
  [InlineData(7, 2, "Pack", new string[] {"Packing Instructions",  "Packing Equipment", "Packing"})]
  public void GetMesPhase(int testOperationNumber, int testPhaseNumber, string expectedPhaseDescription, string[] expectedStepDescriptions) {
    var sut = GetSubjectUnderTest(testOperationNumber);
    var testEditKeys = GetEditKeysForPhase(testOperationNumber, testPhaseNumber);

    var actual = sut.GetMesPhase(testEditKeys);

    actual.Description1.Should().Be(expectedPhaseDescription);
    actual.Steps
      .Select(st => st.Description1)
      .ToArray()
      .Should().BeEquivalentTo(expectedStepDescriptions);
  }

  [Fact]
  public void GetMesPhase_ThrowsProgramException_WhenInvalidPhaseNumber() {
    var testOperationNumber = 7;
    var testPhaseNumber = 10;
    var testEditKeys = GetEditKeysForPhase(testOperationNumber, testPhaseNumber);
    var sut = GetSubjectUnderTest(testOperationNumber);

    Assert.Throws<ProgramException>(() => sut.GetMesPhase(testEditKeys));
  }
  
}