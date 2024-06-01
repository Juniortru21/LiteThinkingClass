using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Common.Exceptions;
internal sealed class ForbiddenExceptionHandler(ILogger<ForbiddenExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<ForbiddenExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (httpContext.Response.StatusCode != StatusCodes.Status403Forbidden)
        {
            return false;
        }

        _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Server Error",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
