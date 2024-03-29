namespace Domain.Entities;

public class MesFormula
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Edition { get; set; }
    public int Revision { get; set; }
    public string Description1 { get; set; }
    public string Description2 { get; set; }
    public string CopyCommand { get; set; }
    public List<MesOperation> Operations { get; set; }
    public List<MesNestedEditorType> NestedEditorTypes { get; set; }
    public DateTime SavedOn { get; set; }
    public string SavedBy { get; set; }

    public string Description {
        get {
            var isDesc1 = !string.IsNullOrWhiteSpace(Description1);
            var isDesc2 = !string.IsNullOrWhiteSpace(Description2);
            if (isDesc1 && isDesc2) return $"{Description1} / {Description2}";
            if (isDesc2) return Description2;

            return Description1;
        }
    }
    public string FormulaName => $"{Name}/{Edition}.{Revision}";
	public bool IsFinished => Operations.All(st => st.IsFinished);

    public bool HasOperation(int operationNumber)
    {
        return Operations.Any(op => op.Number == operationNumber);
    }

    public MesOperation GetMesOperation(MesFormulaEditKeys keys)
    {
        if (keys == null)
        {
            throw new ProgramException("Mes formula edit keys cannot be null.");
        }

        var operation = Operations.FirstOrDefault(op => op.Number == keys.OperationNumber);
        if (operation == null)
        {
            throw new ProgramException($"Mes operation number [{keys.OperationNumber}] cannot be found.");
        }

        return operation;
    }

    public MesOperation GetMesOperation(int operationNumber)
    {
        var operation = Operations.FirstOrDefault(op =>
            op.Number == operationNumber);

        return operation;
    }

    public bool IsFirstOperation(int operationNumber)
    {
        var first = Operations
            .OrderBy(op => op.Number)
            .FirstOrDefault();

        return first != null && first.Number == operationNumber;
    }

    public MesPhase GetMesPhase(MesFormulaEditKeys keys)
    {
        if (keys == null)
        {
            throw new ProgramException("Mes formula edit keys cannot be null.");
        }

        var operation = GetMesOperation(keys);
        var phase = operation.GetMesPhase(keys);

        return phase;
    }

    public MesPhase GetMesPhase(int operationNumber, int phaseNumber)
    {
        var keys = new MesFormulaEditKeys {
            OperationNumber = operationNumber,
            PhaseNumber = phaseNumber
        };

        return GetMesPhase(keys);
    }

    public bool IsFirstPhase(int operationNumber, int phaseNumber)
    {
        var operation = GetMesOperation(operationNumber);
        if (operation == null) return false;

        var first = operation.Phases
            .OrderBy(ph => ph.Number)
            .FirstOrDefault();

        return first != null && first.Number == phaseNumber;
    }

    public MesStep GetMesStep(MesActionKeys actionKeys)
    {
        if (actionKeys == null)
        {
            throw new ProgramException("Mes action keys cannot be null.");
        }

        var editKeys = new MesFormulaEditKeys(actionKeys);

        return GetMesStep(editKeys);
    }

    public MesStep GetMesStep(MesFormulaEditKeys keys)
    {
        if (keys == null)
        {
            throw new ProgramException("Mes formula edit keys cannot be null.");
        }

        var operation = GetMesOperation(keys);
        var phase = operation.GetMesPhase(keys);
        var step = phase.GetMesStep(keys);

        return step;
    }

    public MesStep GetMesStep(int operationNumber, int phaseNumber, int stepNumber)
    {
        var keys = new MesFormulaEditKeys {
            OperationNumber = operationNumber,
            PhaseNumber = phaseNumber,
            StepNumber = stepNumber
        };

        return GetMesStep(keys);
    }

    public IList<MesStep> GetMesStepAndPreviousSteps(MesActionKeys actionKeys)
    {
        if (actionKeys == null)
        {
            throw new ProgramException("Mes action keys cannot be null.");
        }

        var editKeys = new MesFormulaEditKeys(actionKeys);

        return GetMesStepAndPreviousSteps(editKeys);
    }

    public IList<MesStep> GetMesStepAndPreviousSteps(MesFormulaEditKeys keys)
    {
        if (keys == null)
        {
            throw new ProgramException("Mes formula edit keys cannot be null.");
        }

        var operation = GetMesOperation(keys);
        var phase = operation.GetMesPhase(keys);
        var steps = phase.GetMesStepAndPreviousSteps(keys);

        return steps;
    }

    public MesStep GetFirstStep(MesFormulaEditKeys keys)
    {
        if (keys == null)
        {
            throw new ProgramException("Mes formula edit keys cannot be null.");
        }

        var operation = GetMesOperation(keys);
        var phase = operation.GetMesPhase(keys);
        var step = phase.GetFirstStep();

        return step;
    }

    public MesSubStep GetMesSubStep(MesFormulaEditKeys keys)
    {
        if (keys == null)
        {
            throw new ProgramException("Mes formula edit keys cannot be null.");
        }

        var operation = GetMesOperation(keys);
        var phase = operation.GetMesPhase(keys);
        var step = phase.GetMesStep(keys);
        var subStep = step.GetMesSubStep(keys);

        return subStep;
    }

    public MesProperty GetMesProperty(MesFormulaEditKeys keys)
    {
        if (keys == null)
        {
            throw new ProgramException("Mes formula edit keys cannot be null.");
        }

        var operation = GetMesOperation(keys);
        var phase = operation.GetMesPhase(keys);
        var step = phase.GetMesStep(keys);
        var subStep = step.GetMesSubStep(keys);

        MesProperty property = null;
        if (keys.CanSearchProperty)
        {
            property = subStep.Properties.FirstOrDefault(pr => pr.Name == keys.PropertyName);
        }
        else if (keys.CanSearchNestedPropertyChild)
        {
            var subStepProperty = subStep.Properties.FirstOrDefault(pr => pr.Name == keys.NestedPropertyName);
            if (subStepProperty == null
                || !subStepProperty.IsNestedEditorType) return null;

            var nestedEditorType = NestedEditorTypes.FirstOrDefault(editor => editor.Name == subStepProperty.NestedEditorName);
            if (nestedEditorType == null) return null;

            property = nestedEditorType.Properties.FirstOrDefault(pr => pr.Name == keys.PropertyName);
        }
        else
        {
            property = subStep.Properties.FirstOrDefault(pr => pr.Name == keys.NestedPropertyName);
        }

        return property;
    }

    public MesNestedEditorType GetMesNestedEditorType(MesFormulaEditKeys keys)
    {
        if (keys == null)
        {
            throw new ProgramException("Mes formula edit keys cannot be null.");
        }

        var nestedEditorType = NestedEditorTypes
            .FirstOrDefault(net => net.Number == keys.NestedEditorTypeNumber);
        if (nestedEditorType == null)
        {
            throw new ProgramException($"No Mes nested editor type found for [{keys.NestedEditorTypeNumber}]");
        }

        return nestedEditorType;
    }

    public void SetHasPersistedInsertedMesEvents(List<string> propertyNames)
    {
        var insertedEventPropertyNames = propertyNames
            .Where(pn => MesProperty.IsInsertedMesEventPropertyName(pn))
            .ToList();

        foreach (var propertyName in insertedEventPropertyNames)
        {
            (var operationNumber, var phaseNumber, var stepNumber) = MesProperty.GetStagesForMesEventPropertyName(propertyName);
            var keys = new MesFormulaEditKeys
            {
                OperationNumber = operationNumber,
                PhaseNumber = phaseNumber,
                StepNumber = stepNumber,
            };
            var mesStep = GetMesStep(keys);
            if (mesStep != null)
            {
                mesStep.SetHasPersistedMesEvents();
            }
        }
    }

    public void SetIsFinishedSteps(List<MesResult> mesResults)
    {
        var finishedSteps = mesResults
            .Where(x => MesActionKeys.IsFinishedStepPropertyName(x.PropertyName))
            .ToList();

        foreach (var item in finishedSteps)
        {
            var keys = new MesFormulaEditKeys {
                OperationNumber = item.OperationNumber,
                PhaseNumber = item.PhaseNumber,
                StepNumber = item.StepNumber
            };

            var mesStep = GetMesStep(keys);
            if (mesStep != null)
            {
                mesStep.IsFinished = true;
            }
        }
    }
}
