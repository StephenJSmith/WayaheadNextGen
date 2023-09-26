namespace Domain.Enums;

public enum SecurityLevel
{
  Disabled,
  Enabled,
  ErrorWarning,
  WarningWithDefaultButtonYes,
  WarningWithDefaultButtonNo,
  ESignEveryone,
  ESignAuthorisedUsers,
  ESign2ndPerson,
  ESign2ndAuthorisedUsers,
  ESignNotCurrentLogin,
  ESignFormulaPreparation,

  ESignQuality,

  Pass = 1000
}
