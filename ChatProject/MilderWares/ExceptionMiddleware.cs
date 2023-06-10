using Chat.Application.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Chat.Api.MilderWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string result = JsonConvert.SerializeObject(new ErrorDeatils
            {
                ErrorMessage = exception.Message,
                ErrorType = "Failure"
            });

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case ArgumentNullException argumentNullException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(argumentNullException.Message);
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(unauthorizedAccessException.Message);
                    break;
                case SecurityTokenException securityTokenException:
                    statusCode = HttpStatusCode.Forbidden;
                    break;
                case FileNotFoundException fileNotFoundException:
                    statusCode = HttpStatusCode.Conflict;
                    result = JsonConvert.SerializeObject(fileNotFoundException.Message);
                    break;
                default:
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }

    public class ErrorDeatils
    {
        public string? ErrorType { get; set; }
        public string? ErrorMessage { get; set; }
    }

}
