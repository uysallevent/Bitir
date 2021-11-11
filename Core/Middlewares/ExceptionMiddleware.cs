using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IActionResultExecutor<ObjectResult> executor;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();

        public ExceptionMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.executor = executor;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BadRequestException ex)
            {
                _logger.LogError(ex, ex.Message);
                if (context.Response.HasStarted)
                {
                    throw;
                }
                await ErrorHandle(context, ex, ErrorType.badRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unhandled exception has occurred while executing the request.");
                if (context.Response.HasStarted)
                {
                    throw;
                }
                await ErrorHandle(context, ex, ErrorType.internalServer);
            }
        }

        private async Task ErrorHandle(HttpContext context, Exception ex, ErrorType type)
        {
            ObjectResult result;
            var routeData = context.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);
            if (type == ErrorType.badRequest)
            {
                result = new ObjectResult(new ErrorResponse(ex.Message))
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            else
            {
                result = new ObjectResult(new ErrorResponse("Something went wrong"))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }

            await executor.ExecuteAsync(actionContext, result);
        }


        public class ErrorResponse
        {

            public string Message { get; set; }

            public ErrorResponse(string message)
            {
                Message = message;
            }
        }

        public enum ErrorType
        {
            internalServer,
            badRequest
        }
    }
}
