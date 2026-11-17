using RestfulCatalog.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace RestfulCatalog.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<GlobalExceptionMiddleware>
            _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleException(
                    context,
                    ex.Message,
                    HttpStatusCode.NotFound);
            }
            catch (ValidationException ex)
            {
                await HandleException(
                    context,
                    ex.Message,
                    HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleException(
                    context,
                    ex.Message,
                    HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleException(
            HttpContext context,
            string message,
            HttpStatusCode statusCode)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)statusCode;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    new
                    {
                        StatusCode = (int)statusCode,
                        Message = message
                    }));
        }
    }
}
