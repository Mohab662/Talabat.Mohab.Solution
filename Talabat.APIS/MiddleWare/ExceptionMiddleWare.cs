using System.Net;
using System.Text.Json;
using Talabat.APIS.Errors;

namespace Talabat.APIS.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger=logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await httpContext.Response.WriteAsync(json);
            }



        }

    }
}
