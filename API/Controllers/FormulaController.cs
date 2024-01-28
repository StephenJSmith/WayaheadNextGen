using Application.Formula;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FormulaController : BaseApiController
{
  // TODO: Start with single formula - then supply name, edition, version
  [HttpGet]
  public async Task<IActionResult> GetFormula()
  {
    return HandleResult(await Mediator.Send(new DetailsHandler.Query()));
  }
}