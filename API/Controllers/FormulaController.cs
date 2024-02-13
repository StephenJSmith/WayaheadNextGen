using Application.Formula;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FormulaController : BaseApiController
{
  // private const string TestPathFile =  @"d:\_projects\Wayahead\WayaheadNextGen\Formulae/901020.xml";
  private string TestPathFile => OperatingSystem.IsWindows()
    ? @"d:\_projects\Wayahead\WayaheadNextGen\Formulae/901020.xml"
    : @"/Users/macmini/Documents/_projects/Wayahead/WayaheadNextGen/Formulae/901020.xml";
  private const string TestFormulaName = "901020";
  private const int TestEdition = 1;
  private const int TestRevision = 1;

  // TODO: Start with single formula - then supply name, edition, version
  [HttpGet]
  public async Task<IActionResult> GetFormula()
  {
    return HandleResult(await Mediator.Send(new DetailsHandler.Query
    {
      FormulaName = TestFormulaName,
      Edition = TestEdition,
      Revision = TestRevision,
      PathFile = TestPathFile
    }));
  }
}