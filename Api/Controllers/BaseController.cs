using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Common.Results;
using Shared.Core;

namespace Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => this._mediator ??= EngineContext.Current.Resolve<IMediator>();

    protected IMapper Mapper => EngineContext.Current.Resolve<IMapper>();

    protected ActionResult FromResult<T>(Result<T> result) => result.ResultType switch
    {
        ResultType.Ok => this.Ok(result.Data),
        ResultType.NotFound => this.NotFound(result.Errors),
        ResultType.Invalid => this.BadRequest(result.Errors),
        ResultType.Unexpected => this.BadRequest(result.Errors),
        ResultType.PartialOk => this.Ok(result.Data),
        ResultType.Created => this.Created(string.Empty, result.Data),
        _ => throw new Exception("Unhandled result."),
    };
}
