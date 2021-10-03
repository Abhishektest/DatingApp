using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
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
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}



//public async Task InvokeAsync(HttpContext context, object PropertyNamingPolicy)
//{
//    try
//    {
//        await _next(context); // we get conetext and pass to next piece of middleware.  It lives at the top. So if any middlewre throws the exception then it will flow to the top i.e this place thus here we catch the exception
//    }
//    catch(Exception ex)
//    {
//        _logger.LogError(ex, ex.Message);
//        context.Response.ContentType = "application/json";  // we will write out this exception to our response.
//        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//        var response = _env.IsDevelopment()  //we check if dev mode then we API exception using API exception class
//            ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
//            : new ApiException(context.Response.StatusCode, "Internal Server Error"); // for Prod envi

//        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
//        var json = JsonSerializer.Serialize(response, options);  // response in jsonformat.
//        await context.Response.WriteAsync(json);
//    }
//}
