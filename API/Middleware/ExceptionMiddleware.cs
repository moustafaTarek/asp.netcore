using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly IHostEnvironment _env ;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _logger = logger;
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType  = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                        ? new ApiException((int) HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                        : new ApiException((int) HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }
        }

    }
}