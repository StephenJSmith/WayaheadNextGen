using Domain.Entities;

namespace Persistence.Repositories;

public class LoadProgress
{
    public bool IsNestedEditorTypesLoad { get; private set; }
    public string Formula { get; private set; }
    public int Edition { get; private set; }
    public int Revision { get; private set; }
    public int OperationNumber { get; private set; }
    public int PhaseNumber { get; private set; }
    public int StepNumber { get; private set; }
    public int SubStepNumber { get; private set; }
    public int PropertyNumber { get; private set; }
    public int NestedEditorTypeNumber { get; private set; }
    public string OperationDescription1 { get; private set; }
    public string OperationDescription2 { get; private set; }
    public string PhaseDescription1 { get; private set; }
    public string PhaseDescription2 { get; private set; }
    public string NestedEditorTypeName { get; private set; }
    public string HieararchicalPhaseNumber => $"{OperationNumber}.{PhaseNumber}";
    public string HierarchicalStepNumber => $"{OperationNumber}.{PhaseNumber}.{StepNumber}";
    public string HierarchicalSubStepNumber => $"{OperationNumber}.{PhaseNumber}.{StepNumber}.{SubStepNumber}";
    public int EventSubStepNumber => 0;
    public int EventPropertyNumber => 0;
    public string HierarchicalEventSubStepNumber => $"{OperationNumber}.{PhaseNumber}.{StepNumber}.{EventSubStepNumber}";
    public string EventPropertyName => $"$Event_{OperationNumber}.{PhaseNumber}.{StepNumber}";

    public LoadProgress(string formula, int edition, 
        int revision, bool isNestedEditorTypesLoad = false)
    {
        Formula = formula;
        Edition = edition;
        Revision = revision;
        IsNestedEditorTypesLoad = isNestedEditorTypesLoad;
    }

    public void InitialiseOperationNumber()
    {
        OperationNumber = 1;
    }

    public void IncrementOperationNumber()
    {
        OperationNumber++;
    }

    public void SetOperationDescriptions(MesOperation mesOperation)
    {
        OperationDescription1 = mesOperation.Description1;
        OperationDescription2 = mesOperation.Description2;
    }

    public void SetPhaseDescriptions(MesPhase mesPhase)
    {
        PhaseDescription1 = mesPhase.Description1;
        PhaseDescription2 = mesPhase.Description2;
    }

    public void SetStepAncestorDescriptions(MesStep mesStep)
    {
        mesStep.OperationDescription1 = OperationDescription1;
        mesStep .OperationDescription2 = OperationDescription2;
        mesStep.PhaseDescription1 = PhaseDescription1;
        mesStep.PhaseDescription2 = PhaseDescription2;
    }

    public void SetNestedEditorTypeName(string nestedEditorTypeName)
    {
        NestedEditorTypeName = nestedEditorTypeName;
    }

    public void InitialisePhaseNumber()
    {
        PhaseNumber = 1;
    }

    public void IncrementPhaseNumber()
    {
        PhaseNumber++;
    }

    public void InitialiseStepNumber()
    {
        StepNumber = 1;
    }

    public void IncrementStepNumber()
    {
        StepNumber++;
    }

    public void InitialiseSubStepNumber()
    {
        SubStepNumber = 1;
    }

    public void IncrementSubStepNumber()
    {
        SubStepNumber++;
    }

    public void InitialisePropertyNumber()
    {
        PropertyNumber = 1;
    }

    public void IncrementPropertyNumber()
    {
        PropertyNumber++;
    }
    public void InitialiseNestedEditorTypeNumber()
    {
        NestedEditorTypeNumber = 1;
    }

    public void IncrementNestedEditorTypeNumber()
    {
        NestedEditorTypeNumber++;
    }

    public MesFormulaEditKeys GetOperationParentEditKeys()
    {
        return new MesFormulaEditKeys
        {
            Formula = Formula,
            Edition = Edition,
            Revision = Revision,
        };
    }

    public MesFormulaEditKeys GetPhaseParentEditKeys()
    {
        return new MesFormulaEditKeys
        {
            Formula = Formula,
            Edition = Edition,
            Revision = Revision,
            OperationNumber = OperationNumber,
        };
     }

    public MesFormulaEditKeys GetStepParentEditKeys()
    {
        return new MesFormulaEditKeys
        {
            Formula = Formula,
            Edition = Edition,
            Revision = Revision,
            OperationNumber = OperationNumber,
            PhaseNumber = PhaseNumber,
        };
    }

    public MesFormulaEditKeys GetSubStepParentEditKeys()
    {
        return new MesFormulaEditKeys
        {
            Formula = Formula,
            Edition = Edition,
            Revision = Revision,
            OperationNumber = OperationNumber,
            PhaseNumber = PhaseNumber,
            StepNumber = StepNumber,
        };
    }

    public MesFormulaEditKeys GetPropertyParentEditKeys()
    {
        if (IsNestedEditorTypesLoad)
        {
            return GetNestedEditorTypePropertyParentEditKeys();
        }

        return new MesFormulaEditKeys
        {
            Formula = Formula,
            Edition = Edition,
            Revision = Revision,
            OperationNumber = OperationNumber,
            PhaseNumber = PhaseNumber,
            StepNumber = StepNumber,
            SubStepNumber = SubStepNumber,
        };
    }

    public MesFormulaEditKeys GetNestedEditorTypePropertyParentEditKeys()
    {
        return new MesFormulaEditKeys
        {
            Formula = Formula,
            Edition = Edition,
            Revision = Revision,
            NestedEditoryTypeNumber = NestedEditorTypeNumber,
            NestedPropertyName = NestedEditorTypeName
        };
    }

    public MesFormulaEditKeys GetFormulaEditKeys()
    {
        return new MesFormulaEditKeys
        {
            Formula = Formula,
            Edition = Edition,
            Revision = Revision,
        };
    }
}
