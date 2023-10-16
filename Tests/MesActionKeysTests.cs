using Domain.Entities;
using FluentAssertions;

namespace Tests;

    public class MesActionKeysTests
    {

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

            var sut = new MesActionKeys
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

            var sut = new MesActionKeys
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

            var sut = new MesActionKeys
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
    }
