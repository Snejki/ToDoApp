using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using ToDoApp.Db.Exceptions;

namespace ToDoApp.Api.Extensions
{
    public static class GlobalExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger )
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(contextFeature?.Error is SocialAppException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        var response = new
                        {
                            statusCode = context.Response.StatusCode,
                            errors = $"{((SocialAppException)contextFeature.Error).Message}" 
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    logger.LogError(contextFeature.Error.ToString());

                    var r = new
                    {
                        statusCode = context.Response.StatusCode,
                        errors = $"Internal server error"
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(r));
                });
            });
        }
    }
}
