namespace Domain.Entities;

public class MesSubStep
{
	public const int InsertedMesEventSubStepNumber = 0;

	public int Number { get; set; }
	public string HierarchicalNumber { get; set; }
	public MesFormulaEditKeys ParentEditKeys { get; set; }
	public string Name { get; set; }
	public string Description1 { get; set; }
	public string Description2 { get; set; }
	public string CopyCommand { get; set; }
	public List<MesProperty> Properties { get; set; }
	public string PreRenderScript { get; set; }
	public string PostExecutionScript { get; set; }
	public string ReferenceNo { get; set; }
	public bool HasPersistedInsertedMesEvents { get; set; }

	public bool IsMesEvent => Properties != null
		&& Properties.Any(p => p.IsMesEvent);
	public bool IsInsertedMesEvent => Properties != null
		&& Properties.Any(p => p.IsInsertedMesEvent);
	public bool IsShowAllEvents => Properties != null
			&& Properties.Any(p => p.IsShowAllEvents);

	public string SubStepHierarchicalNumber => ParentEditKeys != null
			? $"{ParentEditKeys.OperationNumber}.{ParentEditKeys.PhaseNumber}.{ParentEditKeys.StepNumber}.{Number}"
			: $"?.?.?.{Number}";

	public MesProperty FirstProperty =>
		Properties
			.OrderBy(pr => pr.PropertyNumber)
			.FirstOrDefault();

	public MesProperty GetMesProperty(MesFormulaEditKeys keys)
	{
		if (keys == null)
		{
			throw new ProgramException("Mes formula edit keys cannot be null.");
		}

		MesProperty property = null;
		if (keys.CanSearchProperty)
		{
			property = Properties.FirstOrDefault(prop => prop.Name == keys.PropertyName);
		}
		else if (keys.CanSearchNestedPropertyChild)
		{
			throw new NotImplementedException();
		}
		else
		{
			property = Properties.FirstOrDefault(prop => prop.Name == keys.NestedPropertyName);
		}

		return property;
	}
}