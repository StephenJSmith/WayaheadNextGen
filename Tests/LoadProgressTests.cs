using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;

namespace Tests
{
    public class LoadProgressTests
    {
        private const string TestFormulaName = "901020";
        private const int TestEdition = 1;
        private const int TestRevision = 2;

        [Fact]
        public void Constructor_WhenNoIsNestedEditorTypesArg_ThenIsFalseAsDefault()
        {
            var expected = false;
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);

            var actual = sut.IsNestedEditorTypesLoad;

            actual.Should().Be(expected);
        }

        [Fact]
        public void Constructor_WhenIsNestedEditorTypesArg_ThenPassedValue()
        {
            var testIsNestedEditorType = true;
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision, testIsNestedEditorType);
            var expected = true;

            var actual = sut.IsNestedEditorTypesLoad;

            actual.Should().Be(expected);
        }

        [Fact]
        public void InitialiseOperationNumber_SetsOperationNumberTo1()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = 1;

            sut.InitialiseOperationNumber();

            var actual = sut.OperationNumber;
            actual.Should().Be(expected);
        }

        [Fact]
        public void SetOperationDescriptions()
        {
            var testDescription1 = "Test Operation";
            var testDescription2 = "For Unit Testing";
            var testOperation = new MesOperation {
                Description1 = testDescription1,
                Description2 = testDescription2
             };
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);

            sut.SetOperationDescriptions(testOperation);

            sut.OperationDescription1.Should().Be(testDescription1);
            sut.OperationDescription2.Should().Be(testDescription2);
        }

        [Fact]
        public void SetPhaseDescriptions()
        {
            var testDescription1 = "Test Phase";
            var testDescription2 = "For Unit Testing";
            var testPhase = new MesPhase {
                Description1 = testDescription1,
                Description2 = testDescription2
            };
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);

            sut.SetPhaseDescriptions(testPhase);

            sut.PhaseDescription1.Should().Be(testDescription1);
            sut.PhaseDescription2.Should().Be(testDescription2);
        }

        [Fact]
        public void SetStepAncestorDescriptions()
        {
            var testOpDescription1 = "Test Operation";
            var testOpDescription2 = "Performed by a surgeon";
            var testPhaseDescription1 = "Test Phase";
            var testPhaseDescription2 = "A passing phase";
            var testOperation = new MesOperation {
                Description1 = testOpDescription1,
                Description2 = testOpDescription2
            };
            var testPhase = new MesPhase {
                Description1 = testPhaseDescription1,
                Description2 = testPhaseDescription2
            };
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            sut.SetOperationDescriptions(testOperation);
            sut.SetPhaseDescriptions(testPhase);
            var testStep = new MesStep {
                OperationDescription1 = "Will be overwritten",
                OperationDescription2 = "Will not last long",
                PhaseDescription1 = "Ephemeral",
                PhaseDescription2 = "Fleeting value"
            };

            sut.SetStepAncestorDescriptions(testStep);

            testStep.OperationDescription1.Should().Be(testOpDescription1);
            testStep.OperationDescription2.Should().Be(testOpDescription2);
            testStep.PhaseDescription1.Should().Be(testPhaseDescription1);
            testStep.PhaseDescription2.Should().Be(testPhaseDescription2);
        }

        [Fact]
        public void SetNestedEditorTypeName()
        {
            var testName = "Nested Editor";
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = testName;

            sut.SetNestedEditorTypeName(testName);

            var actual = sut.NestedEditorTypeName;
            actual.Should().Be(expected);
        }

        [Fact]
        public void IncrementOperationNumber() 
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            sut.InitialiseOperationNumber();
            var expected = 2;

            sut.IncrementOperationNumber();

            var actual = sut.OperationNumber;
            actual.Should().Be(expected);
        }

        [Fact]
        public void InitialisePhaseNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = 1;

            sut.InitialisePhaseNumber();

            var actual = sut.PhaseNumber;
            actual.Should().Be(expected);
        }
        
        [Fact]
        public void IncrementPhaseNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            sut.InitialisePhaseNumber();
            var expected = 2;

            sut.IncrementPhaseNumber();

            var actual = sut.PhaseNumber;
            actual.Should().Be(expected);
        }

        [Fact]
        public void InitialiseStepNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = 1;

            sut.InitialiseStepNumber();

            var actual = sut.StepNumber;
            actual.Should().Be(expected);
        }
        
        [Fact]
        public void IncrementStepNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            sut.InitialiseStepNumber();
            var expected = 2;

            sut.IncrementStepNumber();

            var actual = sut.StepNumber;
            actual.Should().Be(expected);
        }

        [Fact]
        public void InitialiseSubStepNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = 1;

            sut.InitialiseSubStepNumber();

            var actual = sut.SubStepNumber;
            actual.Should().Be(expected);
        }
        
        [Fact]
        public void IncrementSubStepNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            sut.InitialiseSubStepNumber();
            var expected = 2;

            sut.IncrementSubStepNumber();

            var actual = sut.SubStepNumber;
            actual.Should().Be(expected);
        }

        [Fact]
        public void InitialisePropertyNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = 1;

            sut.InitialisePropertyNumber();

            var actual = sut.PropertyNumber;
            actual.Should().Be(expected);
        }
        
        [Fact]
        public void IncrementPropertyNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            sut.InitialisePropertyNumber();
            var expected = 2;

            sut.IncrementPropertyNumber();

            var actual = sut.PropertyNumber;
            actual.Should().Be(expected);
        }

        [Fact]
        public void InitialiseNestedEditorTypeNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = 1;

            sut.InitialiseNestedEditorTypeNumber();

            var actual = sut.NestedEditorTypeNumber;
            actual.Should().Be(expected);
        }
        
        [Fact]
        public void IncrementNestedEditorTypeNumber()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            sut.InitialiseNestedEditorTypeNumber();
            var expected = 2;

            sut.IncrementNestedEditorTypeNumber();

            var actual = sut.NestedEditorTypeNumber;
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetOperationParentEditKeys()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = new MesFormulaEditKeys {
                Formula = TestFormulaName,
                Edition = TestEdition,
                Revision = TestRevision
            };
            sut.InitialiseOperationNumber();
            sut.IncrementPropertyNumber();

            var actual = sut.GetOperationParentEditKeys();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetPhaseParentEditKeys()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = new MesFormulaEditKeys {
                Formula = TestFormulaName,
                Edition = TestEdition,
                Revision = TestRevision,
                OperationNumber = 2
            };
            sut.InitialiseOperationNumber();
            sut.IncrementOperationNumber();
            sut.InitialisePhaseNumber();
            sut.IncrementPhaseNumber();
            sut.IncrementPhaseNumber();

            var actual = sut.GetPhaseParentEditKeys();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetStepParentEditKeys()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = new MesFormulaEditKeys {
                Formula = TestFormulaName,
                Edition = TestEdition,
                Revision = TestRevision,
                OperationNumber = 2,
                PhaseNumber = 3
            };
            sut.InitialiseOperationNumber();
            sut.IncrementOperationNumber();
            sut.InitialisePhaseNumber();
            sut.IncrementPhaseNumber();
            sut.IncrementPhaseNumber();
            sut.InitialiseStepNumber();

            var actual = sut.GetStepParentEditKeys();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetSubStepParentEditKeys()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = new MesFormulaEditKeys {
                Formula = TestFormulaName,
                Edition = TestEdition,
                Revision = TestRevision,
                OperationNumber = 2,
                PhaseNumber = 3,
                StepNumber = 1
            };
            sut.InitialiseOperationNumber();
            sut.IncrementOperationNumber();
            sut.InitialisePhaseNumber();
            sut.IncrementPhaseNumber();
            sut.IncrementPhaseNumber();
            sut.InitialiseStepNumber();
            sut.InitialiseSubStepNumber();
            sut.IncrementSubStepNumber();

            var actual = sut.GetSubStepParentEditKeys();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetPropertyParentEditKeys()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = new MesFormulaEditKeys {
                Formula = TestFormulaName,
                Edition = TestEdition,
                Revision = TestRevision,
                OperationNumber = 2,
                PhaseNumber = 3,
                StepNumber = 1,
                SubStepNumber = 2
            };
            sut.InitialiseOperationNumber();
            sut.IncrementOperationNumber();
            sut.InitialisePhaseNumber();
            sut.IncrementPhaseNumber();
            sut.IncrementPhaseNumber();
            sut.InitialiseStepNumber();
            sut.InitialiseSubStepNumber();
            sut.IncrementSubStepNumber();
            sut.InitialisePropertyNumber();
            sut.IncrementPropertyNumber();

            var actual = sut.GetPropertyParentEditKeys();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetNestedEditorTypePropertyParentEditKeys()
        {
            var testEditorTypeName = "Test Editor";
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision, true);
            var expected = new MesFormulaEditKeys {
                Formula = TestFormulaName,
                Edition = TestEdition,
                Revision = TestRevision,
                NestedEditorTypeNumber = 2,
                NestedEditorTypeName = testEditorTypeName
            };
            sut.InitialiseNestedEditorTypeNumber();
            sut.IncrementNestedEditorTypeNumber();
            sut.SetNestedEditorTypeName(testEditorTypeName);
            sut.InitialisePropertyNumber();
            sut.IncrementPropertyNumber();

            var actual = sut.GetNestedEditorTypePropertyParentEditKeys();

            actual.Should().Be(expected);
        }

        [Fact]
        public void GetFormulaEditKeys()
        {
            var sut = new LoadProgress(TestFormulaName, TestEdition, TestRevision);
            var expected = new MesFormulaEditKeys {
                Formula = TestFormulaName,
                Edition = TestEdition,
                Revision = TestRevision,
            };
            sut.InitialiseOperationNumber();
            sut.IncrementOperationNumber();
            sut.InitialisePhaseNumber();
            sut.IncrementPhaseNumber();
            sut.IncrementPhaseNumber();
            sut.InitialiseStepNumber();
            sut.InitialiseSubStepNumber();
            sut.IncrementSubStepNumber();
            sut.InitialisePropertyNumber();
            sut.IncrementPropertyNumber();

            var actual = sut.GetFormulaEditKeys();

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
