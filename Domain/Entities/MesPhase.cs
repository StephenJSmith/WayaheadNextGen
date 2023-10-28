namespace Domain.Entities;

public class MesPhase
{
	public int Number { get; set; }
	public int OperationNumber { get; set; }
	public MesFormulaEditKeys ParentEditKeys { get; set; }
	public string HierarchicalNumber { get; set; }
	public string Name { get; set; }
	public string Description1 { get; set; }
	public string Description2 { get; set; }
	public string CopyCommand { get; set; }
	public List<MesStep> Steps { get; set; }
	public string MenuDescription { get; set; }
	public string NextPhaseAllowed { get; set; }
	public bool DisableFlowControl { get; set; }
	public bool EnableMultiRun { get; set; }
	public string PreRenderScript { get; set; }
	public string PostExecutionScript { get; set; }
	public string ReferenceNo { get; set; }

	public string PhaseHierarchicalNumber => ParentEditKeys != null
			? $"{ParentEditKeys.OperationNumber}.{Number}"
			: $"?.{Number}";
	public bool HasPersistedInsertedMesEvents => Steps.Any(st => st.HasPersistedInsertedMesEvents);
	public bool IsFinished => Steps.All(st => st.IsFinished);

	public MesStep GetMesStep(MesFormulaEditKeys keys)
	{
		if (keys == null)
		{
			throw new ProgramException("Mes formula edit keys cannot be null.");
		}

		var step = Steps.FirstOrDefault(st => st.Number == keys.StepNumber);
		if (step == null)
		{
			throw new ProgramException($"Mes step number [{keys.StepHierarchicalNumber}] cannot be found.");
		}

		return step;
	}

	public IList<MesStep> GetMesStepAndPreviousSteps(MesFormulaEditKeys keys)
	{
		if (keys == null)
		{
			throw new ProgramException("Mes formula edit keys cannot be null.");
		}

		var steps = Steps
			.Where(st => st.Number <= keys.StepNumber)
			.OrderBy(st => st.Number)
			.ToList();

		return steps;
	}

	public MesStep GetFirstStep()
	{
		if (!Steps.Any())
		{
			throw new ProgramException($"No Mes steps");
		}

		return Steps.First();
	}

	public MesActionKeys ToMesActionKeys()
	{
		return new MesActionKeys
		{
			OperationNumber = OperationNumber,
			PhaseNumber = Number
		};
	}

}