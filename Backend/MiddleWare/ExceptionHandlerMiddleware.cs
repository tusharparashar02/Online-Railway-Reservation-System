using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

       public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext){
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log this exception
                logger.LogError(ex, $"{errorId}: {ex.Message}");
                //Return custom errpr response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";     

                var error = new{
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We will try to resolve it",
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }