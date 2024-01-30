export interface Formula {
  id: number;
  name: string;
  edition: number;
  revision: number;
  description1: string;
  description2: string;
  copyCommand: string;
  operations: Operation[];
  nestedEditorTypes: NestedEditorType[];
  savedOn: string;
  savedBy: string;
  description: string;
  formulaName: string;
  isFinished: boolean;
}

export interface Operation {
  number: number;
  description1: string;
  description2: string;
  menuDescription: string;
  nextOperationAllowed: string;
  disableFlowControl: boolean;
  enableMultiRun: boolean;
  preRenderScript: string;
  postExecutionStript: string;
}

export interface Phase {
  number: number;
  description1: string;
  description2: string;
  menuDescription: string;
  nextPhaseAllowed: boolean;
  disableFlowControl: boolean;
  enableMultiRun: boolean;
  preRenderScript: string;
  postExecutionStript: string;
  steps: Step[];
}

export interface ParentEditKeys {
  formula: string;
  edition: number;
  revision: number;
  formulaName: string;
  operationNumber: number;
  phaseNumber: number;
  stepNumber: number;
  subStepNumber: number;
  propertyNumber: number;
  propertyName?: string;
  nestedPropertyNumber?: string;
  nestedPropertyName?: string;
  nestedEditorTypeNumber: number;
  nestedEditorTypeName?: string;
  phaseHierarchicalNumber: string;
  stepHierarchicalNumber: string;
  subStepHierarchicalNumber: string;
  canSearchProperty: boolean;
  canSearchNestedPropertyChild: boolean;
}

export interface Step {
  number: number;
  operationNumber: number;
  phaseNumber: number;
  operationDescription1: string;
  operationDescription2: string;
  phaseDescription1: string;
  phaseDescription2: string;
  hierarchicalNumber: string;
  parentEditKeys: ParentEditKeys;
  name: string;
  description1: string;
  description2: string;
  copyCommand: string;
  subSteps: SubStep[];
  menuDescription: string;
  nextStepAllowed: string;
  enableMultiRun: boolean;
  newRunLinkedSteps?: string;
  alwaysEnableNewRun: boolean;
  securityLevel: number;
  preRenderScript: string;
  postExecutionScript: string;
  referenceNo: string;
  hasPersistedInsertedMesEvents: boolean;
  isFinished: boolean;
  hasInsertedMesEventSubStep: boolean;
  mesEventSubStepNumber: number;
  stepHierarchicalNumber: string;
}

export interface SubStep {
  number: number;
  hierarchicalNumber: string;
  parentEditKeys: ParentEditKeys;
  name?: string;
  description1: string;
  description2?: string;
  copyCommand?: string;
  properties: Property[];
  preRenderScript?: string;
  postExecutionScript?: string;
  referenceNo?: string;
  hasPersistedInsertedMesEvents: boolean;
  isMesEvent: boolean;
  isInsertedMesEvent: boolean;
  isShowAllEvents: boolean;
  subStepHierarchicalNumber: string;
}

export interface Property {
  name: string;
  nestedEditorName?: string;
  propertyNumber: number;
  description1: string;
  description2: string;
  copyCommand?: string;
  hint?: string;
  autoCalculation?: string;
  validation?: string;
  repeatMinTimes: number;
  repeatMaxTimes: number;
  repeatInterval: string;
  valueRange?: string;
  securityLevel: number;
  eSignCommentsRequired: boolean;
  preRenderScript?: string;
  postExecutionScript?: string;
  referenceNo?: string;
  checkCompletion: boolean;
  completionErrorMessage: string;
  postDeletionScript?: string;
  editorType: number;
  defaultValue: string;
  pivot: boolean;
  uom?: string;
  runProgram1?: string;
  runProgram2?: string;
  iFrameUrl: string;
  iFrameUrl2?: string;
  iFramePosition: number;
  childReport: string;
  fullSize: boolean;
  pictureEvidence: boolean;
  disable: boolean;
  hide: boolean;
  nullable: boolean;
  reportType: number;
  myProperty: number;
  parentEditKeys: ParentEditKeys;
  reportCsvStyle: number;
  reportCsvFontSize: number;
  data: ParentEditKeys;
  isMesEvent: boolean;
  isInsertedMesEvent: boolean;
  isShowAllEvents: boolean;
  isNestedEditorType: boolean;
}

export interface NestedEditorTypes {
  name: string;
  description1: string;
  description2: string;
  properties: Property[];
}

