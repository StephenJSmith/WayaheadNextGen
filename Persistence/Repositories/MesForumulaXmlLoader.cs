using System.Xml;
using System.Xml.Linq;
using Domain.Entities;
using Domain.Enums;

namespace Persistence.Repositories;

public class MesFormulaXmlLoader : IMesFormulaXmlLoader {
  private const string FormulaXml = "Formula";
  private const string OperationXml = "Operation";
  private const string PropertyXml = "Property";
  private const string NestedEditorTypeXml = "NestedEditorType";
  private const string NestedEditorNameXml = "NestedEditorName";
  private const string PhaseXml = "Phase";
  private const string StepXml = "Step";
  private const string SubStepXml = "SubStep";

  private const string NumberXml = "Number";
  private const string NameXml = "Name";
  private const string Description1Xml = "Description1";
  private const string Description2Xml = "Description2";
  private const string CopyCommandXml = "CopyCommand";
  private const string MenuDescriptionXml = "MenuDescription";
  private const string DisableFlowControlXml = "DisableFlowControl";
  private const string EnableMultiRunXml = "EnableMultiRun";
  private const string PreRenderScriptXml = "PreRenderScript";
  private const string PostExecutionScriptXml = "PostExecutionScript";
  private const string ReferenceNoXml = "ReferenceNo";
  private const string SecurityLevelXml = "SecurityLevel";

  #region GetFormulaStepsWithEventSubStep()

  public MesFormula GetFormulaStepsWithEventSubStep(
    string formulaName, int edition, int revision, string formulaPathFile)
  {
    if (!Path.Exists(formulaPathFile)) throw new FileNotFoundException();

    try
    {
      var xElement = XElement.Load(formulaPathFile);

      var mesFormula = GetFormula(xElement, formulaName, edition, revision);
      var loadProgress = new LoadProgress(formulaName, edition, revision);
      mesFormula.Operations = GetOperations(xElement, loadProgress);

      var nestedLoadProgress = new LoadProgress(formulaName, edition, revision, true);
      mesFormula.NestedEditorTypes = GetNestedEditorTypes(xElement, nestedLoadProgress);

      return mesFormula;
    }
    catch (Exception ex)
    {
      throw new XmlException();
    }
  }

  private List<MesOperation> GetOperations(XElement xElement, LoadProgress loadProgress)
  {
    try
    {
      var mesOperations = new List<MesOperation>();
      loadProgress.InitialiseOperationNumber();

      foreach (var operationXEl in xElement.Descendants(OperationXml))
      {
        var mesOperation = GetOperation(operationXEl, loadProgress);
        mesOperations.Add(mesOperation);
        loadProgress.IncrementOperationNumber();
      }

      return mesOperations;
    }
    catch (Exception ex)
    {
      throw new XmlException();
    }
  }

  private MesOperation GetOperation(XElement operationXEl,
    LoadProgress loadProgress)
  {
    var operation = GetOperation(operationXEl);
    operation.Number = loadProgress.OperationNumber;
    operation.ParentEditKeys = loadProgress.GetOperationParentEditKeys();

    loadProgress.SetOperationDescriptions(operation);
    operation.Phases = GetPhases(operationXEl, loadProgress);

    return operation;
  }

  private List<MesPhase> GetPhases(XElement operationXEl,
    LoadProgress loadProgress)
  {
    var mesPhases = new List<MesPhase>();
    loadProgress.InitialisePhaseNumber();

    foreach (var phaseXEl in operationXEl.Descendants(PhaseXml))
    {
      var phase = GetPhase(phaseXEl, loadProgress);
      mesPhases.Add(phase);
      loadProgress.IncrementPhaseNumber();
    }

    return mesPhases;
  }

  private MesPhase GetPhase(XElement phaseXEl,
      LoadProgress loadProgress)
  {
    var phase = GetPhase(phaseXEl);
    phase.Number = loadProgress.PhaseNumber;
    phase.OperationNumber = loadProgress.OperationNumber;
    phase.HierarchicalNumber = loadProgress.HieararchicalPhaseNumber;
    phase.ParentEditKeys = loadProgress.GetPhaseParentEditKeys();

    loadProgress.SetPhaseDescriptions(phase);
    phase.Steps = GetSteps(phaseXEl, loadProgress);

    return phase;
  }

  private List<MesStep> GetSteps(XElement phaseXEl, LoadProgress loadProgress)
  {
    var mesSteps = new List<MesStep>();
    loadProgress.InitialiseStepNumber();

    foreach (var stepXEl in phaseXEl.Descendants(StepXml))
    {
      var step = GetStep(stepXEl, loadProgress);
      mesSteps.Add(step);
      loadProgress.IncrementStepNumber();
    }

    return mesSteps;
  }

  private MesStep GetStep(XElement stepXEl, LoadProgress loadProgress)
  {
    var step = GetStep(stepXEl);
    step.Number = loadProgress.StepNumber;
    step.OperationNumber = loadProgress.OperationNumber;
    step.PhaseNumber = loadProgress.PhaseNumber;
    step.HierarchicalNumber = loadProgress.HierarchicalStepNumber;
    step.ParentEditKeys = loadProgress.GetStepParentEditKeys();

    loadProgress.SetStepAncestorDescriptions(step);

    step.SubSteps = GetSubSteps(stepXEl, loadProgress);

    return step;
  }

  private List<MesSubStep> GetSubSteps(XElement stepXEl, LoadProgress loadProgress)
  {
    var mesSubSteps = new List<MesSubStep>();
    loadProgress.InitialiseSubStepNumber();

    foreach (var subStepXEl in stepXEl.Descendants(SubStepXml))
    {
      var subStep = GetSubStep(subStepXEl, loadProgress);
      mesSubSteps.Add(subStep);
      loadProgress.IncrementSubStepNumber();
    }

    if (!mesSubSteps.Any(sub => sub.IsMesEvent))
    {
      mesSubSteps.Insert(0, GetEventSubStep(loadProgress));
    }

    return mesSubSteps;
  }

  private MesSubStep GetEventSubStep(LoadProgress loadProgress)
  {
    var mesSubStep = new MesSubStep
    {
      Number = loadProgress.EventSubStepNumber,
      HierarchicalNumber = loadProgress.HierarchicalEventSubStepNumber,
      Description1 = "Event entry - Please enter any Events / Deviations / Comments in the table below.",
      ParentEditKeys = loadProgress.GetSubStepParentEditKeys(),
      Properties = new List<MesProperty>
            {
                new MesProperty
                {
                    PropertyNumber = loadProgress.EventPropertyNumber,
                    Name = loadProgress.EventPropertyName,
                    ParentEditKeys = loadProgress.GetPropertyParentEditKeys(),
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

  private MesSubStep GetSubStep(XElement subStepXEl, LoadProgress loadProgress)
  {
    var subStep = GetSubStep(subStepXEl);
    subStep.Number = loadProgress.SubStepNumber;
    subStep.HierarchicalNumber = loadProgress.HierarchicalSubStepNumber;
    subStep.ParentEditKeys = loadProgress.GetSubStepParentEditKeys();

    subStep.Properties = GetProperties(subStepXEl, loadProgress);

    return subStep;
  }

  private List<MesProperty> GetProperties(XElement subStepXEl, LoadProgress loadProgress)
  {
    var mesProperties = new List<MesProperty>();
    loadProgress.InitialisePropertyNumber();

    foreach (var propertyXEl in subStepXEl.Descendants(PropertyXml))
    {
      var property = GetProperty(propertyXEl, loadProgress);
      mesProperties.Add(property);
      loadProgress.IncrementPropertyNumber();
    }

    return mesProperties;
  }

  private MesProperty GetProperty(XElement propertyXEl, LoadProgress loadProgress)
  {
    var property = GetProperty(propertyXEl);
    property.PropertyNumber = loadProgress.PropertyNumber;
    property.ParentEditKeys = loadProgress.GetPropertyParentEditKeys();

    return property;
  }

  private List<MesNestedEditorType> GetNestedEditorTypes(XElement xElement, LoadProgress loadProgress)
  {
    var mesNestedEditorTypes = new List<MesNestedEditorType>();
    loadProgress.InitialiseNestedEditorTypeNumber();

    foreach (var xelNested in xElement.Descendants(NestedEditorTypeXml))
    {
      var nestedEditorType = GetNestedEditorType(xelNested, loadProgress);
      mesNestedEditorTypes.Add(nestedEditorType);
      loadProgress.IncrementNestedEditorTypeNumber();
    }

    return mesNestedEditorTypes;
  }

  private MesNestedEditorType GetNestedEditorType(XElement xelNested, LoadProgress loadProgress)
  {
    var nestedType = GetNestedEditorType(xelNested);
    nestedType.Number = loadProgress.NestedEditorTypeNumber;
    nestedType.ParentEditKeys = loadProgress.GetFormulaEditKeys();
    loadProgress.SetNestedEditorTypeName(nestedType.Name);

    nestedType.Properties = GetProperties(xelNested, loadProgress);

    return nestedType;
  }

  #endregion

  #region GetFormulaSteps


  private MesFormula GetFormula(XElement xElement, string formulaName, int edition, int revision)
  {
    var formula = xElement
      .Descendants(FormulaXml)
      .Select(el => new MesFormula
      {
        Name = formulaName,
        Edition = edition,
        Revision = revision,
        Description1 = el.Attribute(Description1Xml).ToStringValue(),
        Description2 = el.Attribute(Description2Xml).ToStringValue(),
        CopyCommand = el.Attribute(CopyCommandXml).ToStringValue(),
        SavedOn = el.Attribute("SavedOn").ToDateTime(),
        SavedBy = el.Attribute("SavedBy").ToStringValue(),
      })
      .FirstOrDefault();

    return formula
      ?? GetEmptyFormula(formulaName, edition, revision);
  }

  private MesFormula GetEmptyFormula(
    string formulaName, int edition, int revision)
  {

    return new MesFormula
    {
      Name = formulaName,
      Edition = edition,
      Revision = revision,
      Description1 = string.Empty,
      Description2 = string.Empty,
      CopyCommand = string.Empty,
      SavedOn = DateTime.MinValue
    };
  }

  private static MesOperation GetOperation(XElement el)
  {
    try
    {
      return new MesOperation
      {
        Number = el.Attribute(NumberXml).ToInt(),
        Name = el.Attribute(NameXml).ToStringValue(),
        Description1 = el.Attribute(Description1Xml).ToStringValue(),
        Description2 = el.Attribute(Description2Xml).ToStringValue(),
        CopyCommand = el.Attribute(CopyCommandXml).ToStringValue(),
        MenuDescription = el.Attribute(MenuDescriptionXml).ToStringValue(),
        NextOperationAllowed = el.Attribute("NextOperationAllowed").ToStringValue(),
        DisableFlowControl = el.Attribute(DisableFlowControlXml).ToBoolean(),
        EnableMultiRun = el.Attribute(EnableMultiRunXml).ToBoolean(),
        PreRenderScript = el.Attribute(PreRenderScriptXml).ToStringValue(),
        PostExecutionScript = el.Attribute(PostExecutionScriptXml).ToStringValue(),
        ReferenceNo = el.Attribute(ReferenceNoXml).ToStringValue()
      };
    }
    catch (Exception ex)
    {
      throw new XmlException();
    }
  }

  private static MesPhase GetPhase(XElement el)
  {
    try
    {
      return new MesPhase
      {
        HierarchicalNumber = el.Attribute(NumberXml).ToStringValue(),
        Number = el.Attribute(NumberXml).FromHierarchicalNumber(),
        Description1 = el.Attribute(Description1Xml).ToStringValue(),
        Description2 = el.Attribute(Description2Xml).ToStringValue(),
        CopyCommand = el.Attribute(CopyCommandXml).ToStringValue(),
        MenuDescription = el.Attribute(MenuDescriptionXml).ToStringValue(),
        NextPhaseAllowed = el.Attribute("NextPhaseAllowed").ToStringValue(),
        DisableFlowControl = el.Attribute(DisableFlowControlXml).ToBoolean(),
        EnableMultiRun = el.Attribute(EnableMultiRunXml).ToBoolean(),
        PreRenderScript = el.Attribute(PreRenderScriptXml).ToStringValue(),
        PostExecutionScript = el.Attribute(PostExecutionScriptXml).ToStringValue(),
        ReferenceNo = el.Attribute(ReferenceNoXml).ToStringValue()
      };
    }
    catch (Exception ex)
    {
      throw new XmlException();
    }
  }

  private static MesStep GetStep(XElement el)
  {
    try
    {
      return new MesStep
      {
        HierarchicalNumber = el.Attribute(NumberXml).ToStringValue(),
        Number = el.Attribute(NumberXml).FromHierarchicalNumber(),
        Name = el.Attribute(NameXml).ToStringValue(),
        Description1 = el.Attribute(Description1Xml).ToStringValue(),
        Description2 = el.Attribute(Description2Xml).ToStringValue(),
        CopyCommand = el.Attribute(CopyCommandXml).ToStringValue(),
        MenuDescription = el.Attribute(MenuDescriptionXml).ToStringValue(),
        NextStepAllowed = el.Attribute("NextStepAllowed").ToStringValue(),
        EnableMultiRun = el.Attribute(EnableMultiRunXml).ToBoolean(),
        AlwaysEnableNewRun = el.Attribute("AlwaysEnableNewRun").ToBoolean(),
        SecurityLevel = el.Attribute(SecurityLevelXml).ToSecurityLevel(),
        PreRenderScript = el.Attribute(PreRenderScriptXml).ToStringValue(),
        PostExecutionScript = el.Attribute(PostExecutionScriptXml).ToStringValue(),
        ReferenceNo = el.Attribute(ReferenceNoXml).ToStringValue()
      };
    }
    catch (Exception ex)
    {
      throw new XmlException();
    }
  }

  private static MesSubStep GetSubStep(XElement el)
  {
    try
    {
      return new MesSubStep
      {
        HierarchicalNumber = el.Attribute(NumberXml).ToStringValue(),
        Number = el.Attribute(NumberXml).FromHierarchicalNumber(),
        Name = el.Attribute(NameXml).ToStringValue(),
        Description1 = el.Attribute(Description1Xml).ToStringValue(),
        Description2 = el.Attribute(Description2Xml).ToStringValue(),
        CopyCommand = el.Attribute(CopyCommandXml).ToStringValue(),
        PreRenderScript = el.Attribute(PreRenderScriptXml).ToStringValue(),
        PostExecutionScript = el.Attribute(PostExecutionScriptXml).ToStringValue(),
        ReferenceNo = el.Attribute(ReferenceNoXml).ToStringValue()
      };
    }
    catch (Exception ex)
    {
      throw new XmlException();
    }
  }

  private static MesNestedEditorType GetNestedEditorType(XElement el)
  {
    return new MesNestedEditorType
    {
      Name = el.Attribute(NameXml).ToStringValue(),
      Description1 = el.Attribute(Description1Xml).ToStringValue(),
      Description2 = el.Attribute(Description2Xml).ToStringValue(),
      Properties = GetProperties(el)
    };
  }

  private static List<MesProperty> GetProperties(XElement parentElement)
  {
    var properties = parentElement
      .Descendants(PropertyXml)
      .Select(el => GetProperty(el))
      .ToList();

    return properties;
  }

  private static MesProperty GetProperty(XElement el)
  {
    try
    {
      return new MesProperty
      {
        Name = el.Attribute(NameXml).ToStringValue(),
        Description1 = el.Attribute(Description1Xml).ToStringValue(),
        Description2 = el.Attribute(Description2Xml).ToStringValue(),
        CopyCommand = el.Attribute(CopyCommandXml).ToStringValue(),
        Hint = el.Attribute("Hint").ToStringValue(),
        AutoCalculation = el.Attribute("AutoCalculation").ToStringValue(),
        Validation = el.Attribute("Validation").ToStringValue(),
        SecurityLevel = el.Attribute(SecurityLevelXml).ToSecurityLevel(),
        PreRenderScript = el.Attribute(PreRenderScriptXml).ToStringValue(),
        PostExecutionScript = el.Attribute(PostExecutionScriptXml).ToStringValue(),
        ReferenceNo = el.Attribute(ReferenceNoXml).ToStringValue(),
        CheckCompletion = el.Attribute("CheckCompletion").ToBoolean(),
        CompletionErrorMessage = el.Attribute("CompletionErrorMessage").ToStringValue(),
        PostDeletionScript = el.Attribute("PostDeletionScript").ToStringValue(),
        EditorType = el.Attribute("EditorType").ToEditorType(),
        DefaultValue = el.Attribute("DefaultValue").ToStringValue(),
        Uom = el.Attribute("UOM").ToStringValue(),
        RunProgram1 = el.Attribute("RunProgram1").ToStringValue(),
        RunProgram2 = el.Attribute("RunProgram2").ToStringValue(),
        IFrameUrl = el.Attribute("IFrameUrl").ToStringValue(),
        IFrameUrl2 = el.Attribute("IFrameUrl2").ToStringValue(),
        IFramePosition = el.Attribute("IframePosition").ToIFramePosition(),
        ChildReport = el.Attribute("ChildReport").ToStringValue(),
        FullSize = el.Attribute("FullSize").ToBoolean(),
        PictureEvidence = el.Attribute("PictureEvidence").ToBoolean(),
        Disable = el.Attribute("Disable").ToBoolean(),
        Hide = el.Attribute("Hide").ToBoolean(),
        Nullable = el.Attribute("Nullable").ToBoolean(),
        ReportType = el.Attribute("ReportType").ToReportType(),

        NestedEditorName = el.Attribute(NestedEditorNameXml).ToStringValue(),
        RepeatMinTimes = el.Attribute("RepeatMinTimes").ToInt(),
        RepeatMaxTimes = el.Attribute("RepeatMaxTimes").ToInt(),

        RepeatInterval = el.Attribute("RepeatInterval").ToTimeSpan(),
        ReportCsvStyle = el.Attribute("ReportCsvStyle").ToReportCsvStyle(),
        ReportCsvFontSize = el.Attribute("ReportCsvFontSize").ToInt(),

        Data = el.ToDataDictionary(),
      };
    }
    catch (Exception ex)
    {
      throw new XmlException();
    }
  }

  #endregion

}