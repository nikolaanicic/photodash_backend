using Entities.ErrorDescription;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace PhotoDash.Extensions
{
    public static class ErrorMiddlewareExtensions
    {

        public static void ConfigureGlobalErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError=>
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error"
                    }.ToString());
                }
            }));
        }
    }
}
