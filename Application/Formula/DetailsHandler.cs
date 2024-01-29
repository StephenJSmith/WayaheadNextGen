using Application.Core;
using Domain.Entities;
using MediatR;
using Persistence.Repositories;

namespace Application.Formula;

public class DetailsHandler
{
  public class Query : IRequest<Result<MesFormula>>
  { 
    public string PathFile { get; set; }
    public string FormulaName { get; set; }
    public int Edition { get; set; }
    public int Revision { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<MesFormula>>
  {
    private readonly IFormulaStepsRepository _repository;
    public Handler(IFormulaStepsRepository repository)
    {
      _repository = repository;
    }

    public async Task<Result<MesFormula>> Handle(Query request, CancellationToken cancellationToken)
    {
      var mesFormula = await _repository.GetFormulaStepsWithEventSubStepAsync(
        request.FormulaName, 
        request.Edition,
        request.Revision,
        request.PathFile);

      return Result<MesFormula>.Success(mesFormula);
    }
  }
}