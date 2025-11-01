using InventoryV2.Shares;
using System;
using System.Net;

namespace InventoryV2.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        readonly RequestDelegate _next;
        readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next , ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) { 
                 
                await HandleExceptionAsync(context, ex);
            }
        }

        async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");

            Response response =  exception switch {
                ApplicationException _ =>  Response.Failure("Application exception occurred.",HttpStatusCode.BadRequest),
                KeyNotFoundException _ =>  Response.Failure("The request key not found.",HttpStatusCode.NotFound),
                UnauthorizedAccessException _ =>  Response.Failure("Unauthorized.",HttpStatusCode.Unauthorized),
                _ =>  Response.Failure("Internal server error. Please retry later.",HttpStatusCode.InternalServerError)
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
