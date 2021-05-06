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
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> log;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,
        ILogger<ExceptionMiddleware> log, 
        IHostEnvironment env)
        {
            this.next = next;
            this.log = log;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try{
                await next(context);
            }
            catch(Exception ex)
            {
                log.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                
                var response = env.IsDevelopment() 
                    ? new ApiExceptions(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiExceptions(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions{
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}