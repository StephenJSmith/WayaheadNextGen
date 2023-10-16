using Domain.Entities;
using FluentAssertions;

namespace Tests;

public class MesFormulaEditKeysTests
{
  [Fact]
  public void FormulaName_ReturnsFormattedFormulaEditRevision()
  {
    var testFormula = "901020";
    var testEdition = 2;
    var testRevision = 4;

    var sut = new MesFormulaEditKeys
    {
      Formula = testFormula,
      Edition = testEdition,
      Revision = testRevision
    };
    var expected = "901020/2.4";

    var actual = sut.FormulaName;

    actual.Should().Be(expected);
  }

  [Fact]
  public void PhaseHierarchicalNumber_ReturnsPhaseOnlyHierarchicalNumber()
  {
    var testFormula = "901020";
    var testEdition = 2;
    var testRevision = 4;
    var testOperationNumber = 3;
    var testPhaseNumber = 2;
    var testStepNumber = 1;
    var testSubStepNumber = 2;

    var sut = new MesFormulaEditKeys
    {
      Formula = testFormula,
      Edition = testEdition,
      Revision = testRevision,
      OperationNumber = testOperationNumber,
      PhaseNumber = testPhaseNumber,
      StepNumber = testStepNumber,
      SubStepNumber = testSubStepNumber
    };
    var expected = "3.2";

    var actual = sut.PhaseHierarchicalNumber;

    actual.Should().Be(expected);
  }

  [Fact]
  public void StepHierarchicalNumber_ReturnsPhaseOnlyHierarchicalNumber()
  {
    var testFormula = "901020";
    var testEdition = 2;
    var testRevision = 4;
    var testOperationNumber = 3;
    var testPhaseNumber = 2;
    var testStepNumber = 1;
    var testSubStepNumber = 2;

    var sut = new MesFormulaEditKeys
    {
      Formula = testFormula,
      Edition = testEdition,
      Revision = testRevision,
      OperationNumber = testOperationNumber,
      PhaseNumber = testPhaseNumber,
      StepNumber = testStepNumber,
      SubStepNumber = testSubStepNumber
    };
    var expected = "3.2.1";

    var actual = sut.StepHierarchicalNumber;

    actual.Should().Be(expected);
  }

  [Fact]
  public void SubStepHierarchicalNumber_ReturnsPhaseOnlyHierarchicalNumber()
  {
    var testFormula = "901020";
    var testEdition = 2;
    var testRevision = 4;
    var testOperationNumber = 3;
    var testPhaseNumber = 2;
    var testStepNumber = 1;
    var testSubStepNumber = 2;

    var sut = new MesFormulaEditKeys
    {
      Formula = testFormula,
      Edition = testEdition,
      Revision = testRevision,
      OperationNumber = testOperationNumber,
      PhaseNumber = testPhaseNumber,
      StepNumber = testStepNumber,
      SubStepNumber = testSubStepNumber
    };
    var expected = "3.2.1.2";

    var actual = sut.SubStepHierarchicalNumber;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("", true)]
  [InlineData("  ", true)]
  [InlineData(null, true)]
  [InlineData("RCV_Confirm", false)]
  public void CanSearchProperty(string testNestedPropertyName, bool expected)
  {
    var testFormula = "901020";
    var testEdition = 2;
    var testRevision = 4;
    var testOperationNumber = 3;
    var testPhaseNumber = 2;
    var testStepNumber = 1;
    var testSubStepNumber = 2;

    var sut = new MesFormulaEditKeys
    {
      Formula = testFormula,
      Edition = testEdition,
      Revision = testRevision,
      OperationNumber = testOperationNumber,
      PhaseNumber = testPhaseNumber,
      StepNumber = testStepNumber,
      SubStepNumber = testSubStepNumber,
      NestedPropertyName = testNestedPropertyName,
    };

    var actual = sut.CanSearchProperty;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("", "", false)]
  [InlineData("", "RCV_AcknowledgmentESign", false)]
  [InlineData("TST_MedicineTest", "TareWeight", true)]
  public void CanSearchNestedPropertyChild(string testNestedPropertyName, string testPropertyName, bool expected)
  {
    var testFormula = "901020";
    var testEdition = 2;
    var testRevision = 4;
    var testOperationNumber = 3;
    var testPhaseNumber = 2;
    var testStepNumber = 1;
    var testSubStepNumber = 2;

    var sut = new MesFormulaEditKeys
    {
      Formula = testFormula,
      Edition = testEdition,
      Revision = testRevision,
      OperationNumber = testOperationNumber,
      PhaseNumber = testPhaseNumber,
      StepNumber = testStepNumber,
      SubStepNumber = testSubStepNumber,
      NestedPropertyName = testNestedPropertyName,
      PropertyName = testPropertyName,
    };

    var actual = sut.CanSearchNestedPropertyChild;

    actual.Should().Be(expected);
  }
}
