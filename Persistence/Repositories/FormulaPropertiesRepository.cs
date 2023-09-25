using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using Domain.Entities;

namespace Persistence.Repositories;

public class FormulaPropertiesRepository
{
  private const string FormulaXml = "Formula";
  private const string OperationXml = "Operation";
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


  public MesFormula GetFormulaProperties(string formulaPathFile)
  {
    if (!Path.Exists(formulaPathFile)) throw new FileNotFoundException();

    try
    {
      var xElement = XElement.Load(formulaPathFile);

      var mesFormula = GetFormula(xElement);
      var mesOperations = GetOperations(xElement);
      var mesPhases = GetPhases(xElement);
      var mesSteps = GetSteps(xElement);
      var mesSubSteps = GetSubSteps(xElement);
      ApplySubStepProperties(xElement, mesSubSteps);

      mesFormula.Operations = GetHierarchicalElements(mesOperations, mesPhases, mesSteps, mesSubSteps);

      return mesFormula;
    }
    catch (Exception)
    {
      throw new XmlException();
    }
  }

  private MesFormula GetFormula(XElement xElement)
  {
    var formula = xElement
      .Descendants(FormulaXml)
      .Select(el => new MesFormula
      {
        Name = el.Attribute(NameXml).Value,
        Edition = int.Parse(el.Attribute("Edition").Value),
        Revision = int.Parse(el.Attribute("Revision").Value),
        Description1 = el.Attribute(Description1Xml).Value,
        Description2 = el.Attribute(Description2Xml).Value,
        CopyCommand = el.Attribute(CopyCommandXml).Value,
        SavedOn = DateTime.Parse(el.Attribute("SavedOn").Value),
        SavedBy = el.Attribute("SavedBy").Value,
      })
      .FirstOrDefault();

    return formula;
  }

  private List<MesOperation> GetOperations(XElement xElement)
  {
    var result = xElement
      .Descendants(OperationXml)
      .Select(el => new MesOperation
      {
        Number = int.Parse(el.Attribute(NumberXml).Value),
        Name = el.Attribute(NameXml).Value,
        Description1 = el.Attribute(Description1Xml).Value,
        Description2 = el.Attribute(Description2Xml).Value,
        CopyCommand = el.Attribute(CopyCommandXml).Value,
        MenuDescription = el.Attribute(MenuDescriptionXml).Value,
        NextOperationAllowed = el.Attribute("NextOperationAllowed").Value,
        DisableFlowControl = el.Attribute(DisableFlowControlXml).ToBoolean(),
        EnableMultiRun = el.Attribute(EnableMultiRunXml).ToBoolean(),
        PreRenderScript = el.Attribute(PreRenderScriptXml).Value,
        PostExecutionScript = el.Attribute(PostExecutionScriptXml).Value,
        ReferenceNo = el.Attribute(ReferenceNoXml).Value
      })
      .OrderBy(o => o.Number)
      .ToList();

    return result;
  }

  private List<MesPhase> GetPhases(XElement xElement)
  {
    var result = xElement
     .Descendants(PhaseXml)
     .Select(el => new MesPhase
     {
       HierarchicalNumber = el.Attribute(NumberXml).Value,
       Number = el.Attribute(NumberXml).Value.FromHierarchicalNumber(),
       Description1 = el.Attribute(Description1Xml).Value,
       Description2 = el.Attribute(Description2Xml).Value,
       CopyCommand = el.Attribute(CopyCommandXml).Value,
       MenuDescription = el.Attribute(MenuDescriptionXml).Value,
       NextPhaseAllowed = el.Attribute("NextPhaseAllowed").Value,
       DisableFlowControl = el.Attribute(DisableFlowControlXml).ToBoolean(),
       EnableMultiRun = el.Attribute(EnableMultiRunXml).ToBoolean(),
       PreRenderScript = el.Attribute(PreRenderScriptXml).Value,
       PostExecutionScript = el.Attribute(PostExecutionScriptXml).Value,
       ReferenceNo = el.Attribute(ReferenceNoXml).Value
     })
     .OrderBy(ph => ph.HierarchicalNumber)
     .ToList();

    return result;
  }

  private List<MesStep> GetSteps(XElement xElement)
  {
    var result = xElement
      .Descendants(StepXml)
      .Select(el => new MesStep
      {
        HierarchicalNumber = el.Attribute(NumberXml).Value,
        Number = el.Attribute(NumberXml).Value.FromHierarchicalNumber(),
        Name = el.Attribute(NameXml).Value,
        Description1 = el.Attribute(Description1Xml).Value,
        Description2 = el.Attribute(Description2Xml).Value,
        CopyCommand = el.Attribute(CopyCommandXml).Value,
        MenuDescription = el.Attribute(MenuDescriptionXml).Value,
        NextStepAllowed = el.Attribute("NextStepAllowed").Value,
        EnableMultiRun = el.Attribute(EnableMultiRunXml).ToBoolean(),
        AlwaysEnableNewRun = el.Attribute("AlwaysEnableNewRun").ToBoolean(),
        SecurityLevel = el.Attribute(SecurityLevelXml).Value,
        PreRenderScript = el.Attribute(PreRenderScriptXml).Value,
        PostExecutionScript = el.Attribute(PostExecutionScriptXml).Value,
        ReferenceNo = el.Attribute(ReferenceNoXml).Value
      })
      .OrderBy(st => st.HierarchicalNumber)
      .ToList();

    return result;
  }

  private List<MesSubStep> GetSubSteps(XElement xElement)
  {
    var result = xElement
      .Descendants(SubStepXml)
      .Select(el => new MesSubStep
      {
        HierarchicalNumber = el.Attribute(NumberXml).Value,
        Number = el.Attribute(NumberXml).Value.FromHierarchicalNumber(),
        Name = el.Attribute(NameXml).Value,
        Description1 = el.Attribute(Description1Xml).Value,
        Description2 = el.Attribute(Description2Xml).Value,
        CopyCommand = el.Attribute(CopyCommandXml).Value,
        PreRenderScript = el.Attribute(PreRenderScriptXml).Value,
        PostExecutionScript = el.Attribute(PostExecutionScriptXml).Value,
        ReferenceNo = el.Attribute(ReferenceNoXml).Value
      })
      .OrderBy(sub => sub.HierarchicalNumber)
      .ToList();

    return result;
  }

  private void ApplySubStepProperties(XElement xElement, List<MesSubStep> mesSubSteps)
  {
    mesSubSteps.ForEach(sub =>
    {
      var subStepXml = xElement
        .Descendants(SubStepXml)
        .Where(el => el.Attribute(NumberXml).Value == sub.HierarchicalNumber)
        .ToList();

      Debug.WriteLine("SubStep: " + sub.HierarchicalNumber);

      var properties = subStepXml
        .Descendants("Property")
        .Select(el => new MesProperty
        {
          Name = el.Attribute(NameXml).Value,
          Description1 = el.Attribute(Description1Xml).Value,
          Description2 = el.Attribute(Description2Xml).Value,
          CopyCommand = el.Attribute(CopyCommandXml).Value,
          Hint = el.Attribute("Hint").Value,
          AutoCalculation = el.Attribute("AutoCalculation").Value,
          Validation = el.Attribute("Validation").Value,
          SecurityLevel = el.Attribute(SecurityLevelXml).Value,
          ESignCommentsRequired = el.Attribute("ESignCommentsRequired").ToBoolean(),
          PreRenderScript = el.Attribute(PreRenderScriptXml).Value,
          PostExecutionScript = el.Attribute(PostExecutionScriptXml).Value,
          ReferenceNo = el.Attribute(ReferenceNoXml).Value,
          CheckCompletion = el.Attribute("CheckCompletion").ToBoolean(),
          CompletionErrorMessage = el.Attribute("CompletionErrorMessage").Value,
          PostDeletionScript = el.Attribute("PostDeletionScript").Value,
          EditorType = el.Attribute("EditorType").Value.ToEditorType(),
          DefaultValue = el.Attribute("DefaultValue").Value,
          Uom = el.Attribute("UOM").Value,
          RunProgram1 = el.Attribute("RunProgram1").Value,
          RunProgram2 = el.Attribute("RunProgram2").Value,
          IFramePosition = el.Attribute("IframePosition").Value.ToIFramePosition(),
          ChildReport = el.Attribute("ChildReport").Value,
          FullSize = el.Attribute("FullSize").ToBoolean(),
          PictureEvidence = el.Attribute("PictureEvidence").ToBoolean(),
          Disable = el.Attribute("Disable").ToBoolean(),
          Hide = el.Attribute("Hide").ToBoolean(),
          Nullable = el.Attribute("Nullable").ToBoolean(),
          ReportType = el.Attribute("ReportType").Value.ToReportType(),

          // Optional properties per EditorType, ReportType, RunProgramX,..
          Data_RichEditor_Rows = el.Attribute("Data-RichEditor_Rows").ToInt(),
          Data_RichEditor_FontSize = el.Attribute("Data-RichEditor_FontSize").ToInt(),
          Data_ReviewESignature_ESignReasons = el.Attribute("Data-ReviewESignature_ESignReasons")?.Value,
          Data_Dispensing_ShowBatchItemOnly = el.Attribute("Data-Dispensing_ShowBatchItemOnly").ToBoolean(),
          Data_Dispensing_Url = el.Attribute("Data-Dispensing_Url")?.Value,
          Data_Dashboard_Url = el.Attribute("Data-Dashboard_Url")?.Value,
          Data_Dashboard_Name = el.Attribute("Data-Dashboard_Name")?.Value,
          Data_Dropdown_KeyValues = el.Attribute("Data-Dropdown_KeyValues")?.Value,
          Data_YesNo_LimitSelections = el.Attribute("Data-YesNo_LimitSelections").ToBoolean(),
          Data_EquipmentIssuing_CheckStartupEquipmentsOnly = el.Attribute("Data-EquipmentIssuing_CheckStartupEquipmentsOnly").ToBoolean(),
          Data_EquipmentIssuing_Url = el.Attribute("Data-EquipmentIssuing_Url")?.Value,
          Data_Mfg_Url = el.Attribute("Data-Mfg_Url")?.Value,
          Data_Timespan_TimeRange = el.Attribute("Data-Timespan_TimeRange")?.Value,
          Data_Timespan_StartEndTimeProperty = el.Attribute("Data-Timespan_StartEndTimeProperty")?.Value,
          Data_Timespan_TimeSpanFormat = el.Attribute("Data-Timespan_TimeSpanFormat")?.Value,
          Data_Nested_ReportStyle = el.Attribute("Data-Nested_ReportStyle")?.Value,
          Data_Nested_ReportFontSize = el.Attribute("Data-Nested_ReportFontSize").ToInt(),
          Data_Nested_ReportHideCreatedBy = el.Attribute("Data-Nested_ReportHideCreatedBy").ToBoolean(),
          Data_Nested_ReportHideESignatureBy = el.Attribute("Data-Nested_ReportHideESignatureBy").ToBoolean(),
          Data_Nested_ReportHideIsDeleted = el.Attribute("Data-Nested_ReportHideIsDeleted").ToBoolean(),
          Data_ManualIPC_Url = el.Attribute("Data-ManualIPC_Url")?.Value,
          Data_MesEvent_Url = el.Attribute("Data-MesEvent_Url")?.Value,
        })
        .ToList();

      sub.Properties = properties;
    });
  }

  private List<MesOperation> GetHierarchicalElements(
    List<MesOperation> mesOperations,
    List<MesPhase> mesPhases,
    List<MesStep> mesSteps,
    List<MesSubStep> mesSubSteps)
  {
    mesSteps.ForEach(st =>
      st.SubSteps = mesSubSteps
        .Where(sub => sub.HierarchicalNumber.StartsWith(st.HierarchicalNumber))
        .ToList()
    );

    mesPhases.ForEach(ph =>
      ph.Steps = mesSteps
        .Where(st => st.HierarchicalNumber.StartsWith(ph.HierarchicalNumber))
        .ToList()
    );

    mesOperations.ForEach(op =>
      op.Phases = mesPhases.Where(ph => ph.HierarchicalNumber.StartsWith(op.Number.ToString())).ToList()
    );

    return mesOperations;
  }
}