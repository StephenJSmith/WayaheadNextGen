using Domain.Enums;

namespace Domain.Entities;

public class MesStep {
  public int Number { get; set; }
	public int OperationNumber { get; set; }
	public int PhaseNumber { get; set; }
	public string OperationDescription1 { get; set; }
	public string OperationDescription2 { get; set; }
	public string PhaseDescription1 { get; set; }
	public string PhaseDescription2 { get; set; }
	public string HierarchicalNumber { get; set; }
	public MesFormulaEditKeys ParentEditKeys { get; set; }
	public string Name { get; set; }
	public string Description1 { get; set; }
	public string Description2 { get; set; }
	public string CopyCommand { get; set; }
  public List<MesSubStep> SubSteps { get; set; }
	public string MenuDescription { get; set; }
	public string NextStepAllowed { get; set; }
	public bool EnableMultiRun { get; set; }
  public string NewRunLinkedSteps { get; set; }
  public bool AlwaysEnableNewRun { get; set; }
	public SecurityLevel SecurityLevel { get; set; }
	public string PreRenderScript { get; set; }
	public string PostExecutionScript { get; set; }
	public string ReferenceNo { get; set; }
	public bool HasInsertedMesEventSubStep => 
		SubSteps != null && SubSteps.Any(sub => sub.IsInsertedMesEvent);
	public int MesEventSubStepNumber => HasInsertedMesEventSubStep
		? SubSteps.First(sub => sub.IsMesEvent).Number
		: -1;

	public string StepHierarchicalNumber => ParentEditKeys != null
			? $"{ParentEditKeys.OperationNumber}.{ParentEditKeys.PhaseNumber}.{Number}"
			: $"?.?.{Number}";

	public MesSubStep GetMesSubStep(MesFormulaEditKeys keys) {
		if (keys == null)
		{
			throw new ProgramException("Mes formula edit keys cannot be null.");
		}

		var subStep = SubSteps.FirstOrDefault(sub => sub.Number == keys.SubStepNumber);
		if (subStep == null) {
			throw new ProgramException($"Mes sub step number [{keys.SubStepHierarchicalNumber}] cannot be found.");
		}

		return subStep;
	}

	public void CreateRequiredMesEventSubStep() {
		if (SubSteps.Any(sub => sub.IsMesEvent)) return;
		if (SubSteps.Any(sub => sub.IsInsertedMesEvent)) return;

    SubSteps.Insert(0, GetEventSubStep());
	}

        private MesSubStep GetEventSubStep()
        {
            var parentEditKeys = ParentEditKeys.Clone();
            parentEditKeys.StepNumber = Number;

            var mesSubStep = new MesSubStep
            {
                Number = 0,
                Description1 = "Event entry - Please enter any Events / Deviations / Comments in the table below.",
                ParentEditKeys = parentEditKeys,
                Properties = new List<MesProperty> {
                    new MesProperty
                    {
                        Name = $"$Event_{ParentEditKeys.OperationNumber}.{ParentEditKeys.PhaseNumber}.{Number}",
                        Description1 = "",
                        Description2 = "",
                        ParentEditKeys = ParentEditKeys.Clone(),
                        SecurityLevel = SecurityLevel.Disabled,
                        CheckCompletion = true,
                        CompletionErrorMessage = "Please select YES to indicate that all entries have been added to table.",
                        EditorType = MesEditorType.YesNo,
                        DefaultValue = "Yes",
                        IFrameUrl = "/MesAuxiliary/MesEvent/",
                        IFramePosition = MesIFramePosition.After,
                        ChildReport = "MesEventReport.frx",
                        FullSize = true,
                        PictureEvidence = false,
                        Disable = false,
                        Hide = true,
                        Nullable = false,
                        ReportType = MesReportType.StandardAndChildReport,
                        ReportCsvStyle = 0,
                        ReportCsvFontSize = 0,
                    }
                }
            };

            return mesSubStep;
        }

	public MesActionKeys ToMesActionKeys() {
		return new MesActionKeys {
			OperationNumber = OperationNumber,
			PhaseNumber = PhaseNumber,
			StepNumber = Number
		};
	}
}