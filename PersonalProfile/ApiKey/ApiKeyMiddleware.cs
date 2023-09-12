using Microsoft.AspNetCore.Http;
using PersonalProfile.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PersonalProfile.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace PersonalProfile.ApiKey
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _apiKey = "X-API-Key";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(_apiKey, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No API key was provided.");
                return;
            }

            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = configuration.GetValue<string>(_apiKey);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next(context);
        }
    }
}
