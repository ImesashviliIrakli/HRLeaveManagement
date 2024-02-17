using HRLeaveManagement.API.Models;
using HRLeaveManagement.Application.Exceptions;
using System.Net;

namespace HRLeaveManagement.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        CustomProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;

                problem.Title = badRequestException.Message;
                problem.Status = (int)statusCode;
                problem.Type = nameof(BadRequestException);
                problem.Detail = badRequestException.InnerException?.Message;
                problem.Errors = badRequestException.ValidationErrors;

                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;

                problem.Title = notFoundException.Message;
                problem.Status = (int)statusCode;
                problem.Type = nameof(NotFoundException);
                problem.Detail = notFoundException.InnerException?.Message;

                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;

                problem.Title = ex.Message;
                problem.Status = (int)statusCode;
                problem.Type = nameof(HttpStatusCode.InternalServerError);
                problem.Detail = ex.InnerException?.Message;

                break;
        }

        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(problem);
    }
}
