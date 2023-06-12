using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkelAPI
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorMessage = "An unexpected error occurred.";
            var jsonResult = new JsonResult(errorMessage)
            {
                StatusCode = 500,
                ContentType = "application/json"
            };

            context.Result = jsonResult;
        }
    }
}
