using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using KaedeBackend.Exceptions.Common;
using KaedeBackend.Exceptions;

namespace KaedeBackend.Filters
{
    public class BaseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not BaseException)
            {
                Console.WriteLine($"Unhandled exception: {context.Exception.ToString()}");

                // Unhandled exception
                context.Exception = new UnhandledErrorException(Guid.NewGuid().ToString());
            }

            var exception = (BaseException)context.Exception;
            context.Result = new JsonResult(exception)
            {
                StatusCode = exception.StatusCode
            };
        }
    }
}
