using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    // this expression means: if ClientSideException => return 400; else => return 500
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        UnauthorizedException => 401,
                        ForbiddenException => 403,
                        NotFoundException => 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;

                    var message = statusCode == 500 ? "An error is ocurred. Please try again later." : exceptionFeature.Error.Message;

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
