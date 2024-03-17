using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System;

namespace TranslationManagement.Api.Filters;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is Exception exception)
        {
            context.Result = new ObjectResult(exception.Message)
            {
                StatusCode = 500,
            };

            context.ExceptionHandled = true;
        }
    }
}