﻿using E_Commerce.API.Erorrs;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace E_Commerce.API.Midlleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware > logger,IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
            
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "appliction/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var respose = _env.IsDevelopment() ?
                    new ApiExceptionResponce(StatusCodes.Status500InternalServerError, ex?.Message, ex?.StackTrace?.ToString())
                    : new ApiExceptionResponce(StatusCodes.Status500InternalServerError);
                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(respose,option);
                await context.Response.WriteAsync(json);

            }
        }

    }
}
