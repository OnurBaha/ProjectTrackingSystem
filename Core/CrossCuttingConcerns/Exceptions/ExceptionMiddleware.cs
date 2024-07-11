using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExceptionHandler _httpExceptionHandler;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _httpExceptionHandler = new HttpExceptionHandler();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                
                await _next(context);
            }
            catch (Exception exception)
            {
                Log.Information(exception.Message, "An error occurred while fetching the list of StudentCourses");
                Exception innerException = exception.InnerException; 
                while (innerException != null)
                {
                    Log.Information(innerException.Message, "Inner Exception");
                    innerException = innerException.InnerException;
                }
                await HandleExceptionAsync(context.Response, exception);
            }

        }

        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {


            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }
    }
}