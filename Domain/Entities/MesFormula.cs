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
        var property = subStep.GetMesProperty(keys);

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
        if (nestedEditorType == null) {
            throw new ProgramException($"No Mes nested editor type found for [{keys.NestedEditorTypeNumber}]");
        }

        return nestedEditorType;
    }
}
