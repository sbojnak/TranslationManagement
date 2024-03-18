using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System;
using TranslationManagement.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Api.Filters;

/// <summary>
/// Global exception handling. 
/// Adds information about exception and sets correct status code based on possible error.
/// </summary>
public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly ILogger<HttpResponseExceptionFilter> _logger;
    public int Order => int.MaxValue - 10;

    public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if(context.Exception == null)
        {
            return;
        }

        _logger.LogError("Handling exception {ExceptionType} with message {ExceptionMessage}", context.Exception.GetType(),
            context.Exception.Message);

        context.Result = new ObjectResult(context.Exception.Message)
        {
            StatusCode = context.Exception switch
            {
                //client errors
                InvalidJobFileException _ => 400,
                InvalidJobStatusChangeException _ => 400,
                InvalidJobIdException _ => 400,
                InvalidTranslatorIdException _ => 400,

                //General error
                Exception _ => 500,
                _ => 500,
            },
        };

        context.ExceptionHandled = true;

    }
}