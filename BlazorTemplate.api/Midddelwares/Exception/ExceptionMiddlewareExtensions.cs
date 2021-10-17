using Entities.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace BlazorTemplate.api.Midddelwares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
     /*   public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                    //    logger.LogError($"Something went wrong: {contextFeature.Error}");

                        var apiResponse = new ApiResponse<string>()
                        {
                            Message = contextFeature.Error.Message,
                            IsSucessFull = false,
                            Payload = null,
                        };

                        var result = JsonSerializer.Serialize(apiResponse);


                        await context.Response.WriteAsync(result);
                    }
                });
            });
        }
   
    */
    }
}
