using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Exceptions
{
    public static class ExceptionMiddlewareExtentions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionErrorHandling>();
        }
    }
}
