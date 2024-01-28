using Application.Core;
using Domain.Entities;
using MediatR;
using Persistence.Repositories;

namespace Application.Formula;

public class DetailsHandler
{
    private const string TestPathFile =  "901020.xml";
    private const string TestFormulaName = "901020";
    private const int TestEdition = 1;
    private const int TestRevision = 1;

  public class Query : IRequest<Result<MesFormula>>
  { }

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
        TestFormulaName, TestEdition, TestRevision, TestPathFile);

      return Result<MesFormula>.Success(mesFormula);
    }
  }
}