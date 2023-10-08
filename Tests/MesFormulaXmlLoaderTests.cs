using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests;

public class MesFormulaXmlLoaderTests
{
  private const string TestPathFile = "901020.xml";
  private const string TestFormulaName = "901020";
  private int TestEdition = 1;
  private int TestRevision = 1;

  #region  GetFormulaStepsWithEventSubStep
  [Fact]
  public void GetFormulaStepsWithEventSubStep()
  {
    var sut = new MesFormulaXmlLoader();

    var actual = sut.GetFormulaStepsWithEventSubStep(TestPathFile, TestFormulaName, TestEdition, TestRevision);
  }

  [Fact]
  public void GetFormulaStepsWithEventSubStep_IncludesExpectedOperations()
  {
    var sut = new MesFormulaXmlLoader();
    var expectedOperationDescriptions = new List<string> {
      "Picking",
      "Check Order",
      "Dispensing",
      "Review",
      "Mixing",
      "Manufacturing",
      "Test and Pack",
      "Batch Completion",
      "Quality Review"
    };

    var actual = sut.GetFormulaStepsWithEventSubStep(TestPathFile, TestFormulaName, TestEdition, TestRevision);

    actual.Operations.Count.Should().Be(expectedOperationDescriptions.Count);
    actual.Operations
      .Select(o => o.Description1)
      .ToList()
      .Should().BeEquivalentTo(expectedOperationDescriptions);
  }

  [Fact]
  public void GetFormulaStepsWithEventSubStep_IncludesExpectedPhases()
  {
    var sut = new MesFormulaXmlLoader();
    var expectedPhases = new Dictionary<int, IList<string>> {
      {1, new List<string>{"Picking" }},
      {2, new List<string>{"Check Order"}},
      {3, new List<string>{"Dispensing"}},
      {4, new List<string>{"Review"}},
      {5, new List<string>{"Mixing"}},
      {6, new List<string>{"Manufacturing"}},
      {7, new List<string>{"Test", "Pack"}},
      {8, new List<string>{"Events/Comments Entry", "Production Batch Close"}},
      {9, new List<string>{"Review"}},
    };

    var actual = sut.GetFormulaStepsWithEventSubStep(TestPathFile, TestFormulaName, TestEdition, TestRevision);

    foreach (var operation in actual.Operations)
    {
      var expectedPhase = expectedPhases[operation.Number];
      operation.Phases
        .Select(ph => ph.Description1)
        .ToList()
        .Should().BeEquivalentTo(expectedPhase);
    }
  }

  [Fact]
  public void GetFormulaStepsWithEventSubStep_IncludesExpectedSteps()
  {
    var sut = new MesFormulaXmlLoader();
    var expectedSteps = new Dictionary<string, IList<string>> {
      {"1.1", new List<string>{"Picking Instructions", "Print Picking Report" }},
      {"2.1", new List<string>{"Check Order Instructions", "Check Batch"}},
      {"3.1", new List<string>{"Dispensing Instructions", "Dispensing Equipment", "Dispense Order"}},
      {"4.1", new List<string>{"Review Instructions", "Review Batch", }},
      {"5.1", new List<string>{"Mixing Instructions", "Mixing Equipment", "Tipping",  "Mixing", "Mixing Bin Transfer"}},
      {"6.1", new List<string>{"Manufacturing Instructions",  "Steam Kettle", "Manufacturing", "BTC Transfer"}},
      {"7.1", new List<string>{"Test Instructions", "Test"}},
      {"7.2", new List<string>{"Packing Instructions", "Packing Equipment", "Packing"}},
      {"8.1", new List<string>{"Mes events / comments"}},
      {"8.2", new List<string>{"Batch CloseOut"}},
      {"9.1", new List<string>{"Quality Review", "Quality CloseOut"}},
    };

    var actual = sut.GetFormulaStepsWithEventSubStep(TestPathFile, TestFormulaName, TestEdition, TestRevision);

    foreach (var item in expectedSteps)
    {
      var elements = item.Key.Split('.');
      var editKeys = new MesFormulaEditKeys {
        Formula = TestFormulaName,
        Edition = TestEdition,
        Revision = TestRevision,
        OperationNumber = int.Parse(elements[0]),
        PhaseNumber = int.Parse(elements[1])
      };

      var phase = actual.GetMesPhase(editKeys);
      phase.Steps
        .Select(st => st.Description1)
        .ToList()
        .Should().BeEquivalentTo(item.Value);
    }
  }

  [Fact]
  public void GetFormulaStepsWithEventSubStep_IncludesExpectedSubStepsPerXmlFile()
  {
    var sut = new MesFormulaXmlLoader();
    var expectedSubSteps = new Dictionary<string, IList<string>> {
      {"1.1.1", new List<string>{"Picking Instructions" }},
      {"1.1.2", new List<string>{"Confirm Batch Details", "Print picking report with barcode" }},
      {"2.1.1", new List<string>{"Check Order Instructions"}},
      {"2.1.2", new List<string>{"Check Batch", "Check Batch Electronic Signature"}},
      {"3.1.1", new List<string>{"Dispensing Instructions"}},
      {"3.1.2", new List<string>{"Dispensing Equipment Issue"}},
      {"3.1.3", new List<string>{"Dispense Order", "Dispense Order Electronic Signature"}},
      {"4.1.1", new List<string>{"Review Instructions", }},
      {"4.1.2", new List<string>{"Review Batch Pictures", "Review Batch Electronic Signature", "Confirm Batch Details"}},
      {"5.1.1", new List<string>{"Mixing Instructions"}},
      {"5.1.2", new List<string>{"Mixing Equipment Issue",}},
      {"5.1.3", new List<string>{"Tip all the dispensing bags into Mixing Bin"}},
      {"5.1.4", new List<string>{"Mixing", "Total Mixing Time", "Mixing Electronic Signature"}},
      {"5.1.5", new List<string>{ "Mixing Bin Transfer", "Mxing bin transfer Electronic Signature"}},
      {"6.1.1", new List<string>{"Manufacturing Instructions"}},
      {"6.1.2", new List<string>{"Steam Kettle Issue" }},
      {"6.1.3", new List<string>{"Manufacturing", "Manufacturing Electronic Signature"}},
      {"6.1.4", new List<string>{"BTC Transfer", "BTC Transfer Electronic Signature"}},
      {"7.1.1", new List<string>{"Test Instructions"}},
      {"7.1.2", new List<string>{"In-Process Test", "In-Process Test Summary"}},
      {"7.2.1", new List<string>{"Packing Instructions"}},
      {"7.2.2", new List<string>{"Packing Equipment Issue"}},
      {"7.2.3", new List<string>{"Packing", "Packing Electronic Signature"}},
      {"8.1.1", new List<string>{"Event entry - Please enter any Events / Deviations / Comments in the table below.", "Event entry - Please enter any Events / Deviations / Comments in the table below."}},
      {"8.2.1", new List<string>{ "Production Batch CloseOut - send for QA Review"}},
      {"9.1.1", new List<string>{"Quality Review", "EBR Process steps", "Quality Review Status"}},
      {"9.1.2", new List<string>{"Quality Batch CloseOut"}},
    };

    var actual = sut.GetFormulaStepsWithEventSubStep(TestPathFile, TestFormulaName, TestEdition, TestRevision);

    foreach (var item in expectedSubSteps)
    {
      var elements = item.Key.Split('.');
      var editKeys = new MesFormulaEditKeys {
        Formula = TestFormulaName,
        Edition = TestEdition,
        Revision = TestRevision,
        OperationNumber = int.Parse(elements[0]),
        PhaseNumber = int.Parse(elements[1]),
        StepNumber = int.Parse(elements[2]),
      };

      var step = actual.GetMesStep(editKeys);
      step.SubSteps
        .Where(sub => !sub.IsInsertedMesEvent)
        .Select(sub => sub.Description1)
        .ToList()
        .Should().BeEquivalentTo(item.Value);
    }
  }

  [Fact]
  public void GetFormulaStepsWithEventSubStep_IncludesExpectedInsertedMesSubSteps()
  {
    var sut = new MesFormulaXmlLoader();
    var expectedInsertedSubSteps = new List<string> {
      "1.1.1.0", "1.1.2.0", "2.1.1.0", "2.1.2.0", "3.1.1.0", "3.1.2.0", "3.1.3.0",
      "4.1.1.0", "4.1.2.0", "5.1.1.0", "5.1.2.0", "5.1.3.0", "5.1.4.0", "5.1.5.0",
      "6.1.1.0", "6.1.2.0", "6.1.3.0", "6.1.4.0", "7.1.1.0", "7.1.2.0", "7.2.1.0",
      "7.2.2.0", "7.2.3.0", "8.2.1.0", "9.1.2.0"
    };

    var actual = sut.GetFormulaStepsWithEventSubStep(TestPathFile, TestFormulaName, TestEdition, TestRevision);

    foreach (var subStep in expectedInsertedSubSteps)
    {
      var elements =subStep.Split('.');
      var editKeys = new MesFormulaEditKeys {
        Formula = TestFormulaName,
        Edition = TestEdition,
        Revision = TestRevision,
        OperationNumber = int.Parse(elements[0]),
        PhaseNumber = int.Parse(elements[1]),
        StepNumber = int.Parse(elements[2]),
        SubStepNumber = int.Parse(elements[3])
      };

      var step = actual.GetMesSubStep(editKeys);
      step.IsInsertedMesEvent.Should().BeTrue();
    }
  }

  [Fact]
  public void GetFormulaStepsWithEventSubStep_IncludesExpectedProperties()
  {
    var sut = new MesFormulaXmlLoader();
    var expectedSubStepPropertyCounts = new Dictionary<string, int> {
      {"1.1.1.0", 1}, {"1.1.1.1", 3}, {"1.1.2.0", 1}, {"1.1.2.1", 1}, {"1.1.2.2", 2},
      {"2.1.1.0", 1}, {"2.1.1.1", 3}, {"2.1.2.0", 1}, {"2.1.2.1", 1}, {"2.1.2.2", 2},
      {"3.1.1.0", 1}, {"3.1.1.1", 3}, {"3.1.2.0", 1}, {"3.1.2.1", 1}, {"3.1.3.0", 1}, 
      {"3.1.3.1", 1}, {"3.1.3.2", 1}, {"4.1.1.0", 1}, {"4.1.1.1", 3}, {"4.1.2.0", 1},
      {"4.1.2.1", 1}, {"4.1.2.2", 1}, {"4.1.2.3", 1}, {"5.1.1.0", 1}, {"5.1.1.1", 3},
      {"5.1.2.0", 1}, {"5.1.2.1", 1}, {"5.1.3.0", 1}, {"5.1.3.1", 1}, {"5.1.4.0", 1},
      {"5.1.4.1", 2}, {"5.1.4.2", 1}, {"5.1.4.3", 1}, {"5.1.5.0", 1}, {"5.1.5.1", 1},
      {"5.1.5.2", 1}, {"6.1.1.0", 1}, {"6.1.1.1", 3}, {"6.1.2.0", 1}, {"6.1.2.1", 1},
      {"6.1.3.0", 1}, {"6.1.3.1", 3}, {"6.1.3.2", 1}, {"6.1.4.0", 1}, {"6.1.4.1", 1},
      {"6.1.4.2", 1}, {"7.1.1.0", 1}, {"7.1.1.1", 3}, {"7.1.2.0", 1}, {"7.1.2.1", 1},
      {"7.1.2.2", 2}, {"7.2.1.0", 1}, {"7.2.1.1", 3}, {"7.2.2.0", 1}, {"7.2.2.1", 1},
      {"7.2.3.0", 1}, {"7.2.3.1", 3}, {"7.2.3.2", 1}, {"8.1.1.1", 2}, {"8.1.1.2", 1},
      {"8.2.1.0", 1}, {"8.2.1.1", 1}, {"9.1.1.1", 1}, {"9.1.1.2", 3}, {"9.1.1.3", 2},
      {"9.1.2.0", 1}, {"9.1.2.1", 1}
    };

    var actual = sut.GetFormulaStepsWithEventSubStep(TestPathFile, TestFormulaName, TestEdition, TestRevision);

    foreach (var item in expectedSubStepPropertyCounts)
    {
      var elements =item.Key.Split('.');
      var editKeys = new MesFormulaEditKeys {
        Formula = TestFormulaName,
        Edition = TestEdition,
        Revision = TestRevision,
        OperationNumber = int.Parse(elements[0]),
        PhaseNumber = int.Parse(elements[1]),
        StepNumber = int.Parse(elements[2]),
        SubStepNumber = int.Parse(elements[3])
      };

      var subStep = actual.GetMesSubStep(editKeys);
      subStep.Properties.Count.Should().Be(item.Value);
    }
  }

  [Fact]
  public void GetFormulaStepsWithEventSubStep_ReturnsOrderedSubSteps_WhenNotMatchingNumber()
  {
    var sut = new MesFormulaXmlLoader();
    var testEditKeys = new MesFormulaEditKeys {
      Formula = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      OperationNumber = 9,
      PhaseNumber = 1,
      StepNumber = 1,
      SubStepNumber = 3
    };
    var expectedSubStepNumber = "9.1.1.3";
    var expectedSubStepDescription1 = "Quality Review Status";
    var expectedXmlSubStepNumber = "9.1.1.5"; // XML file SubStep Number attribute value

    var actual = sut.GetFormulaStepsWithEventSubStep(TestPathFile, TestFormulaName, TestEdition, TestRevision);

    var actualSubStep = actual.GetMesSubStep(testEditKeys);
    actualSubStep.HierarchicalNumber.Should().Be(expectedSubStepNumber);
    actualSubStep.Description1.Should().Be(expectedSubStepDescription1);
  }

  #endregion

  #region  GetFormulaSteps

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

  #endregion
}

