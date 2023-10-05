using System.Runtime.Serialization;

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

    public MesSubStep GetMesSubStep(MesFormulaEditKeys keys) {
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

    public MesProperty? GetMesProperty(MesFormulaEditKeys keys)
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
}

[Serializable]
internal class ProgramException : Exception
{
    public ProgramException()
    {
    }

    public ProgramException(string? message) : base(message)
    {
    }

    public ProgramException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}