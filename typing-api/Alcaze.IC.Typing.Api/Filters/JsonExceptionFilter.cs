using Alcaze.Helper.Exceptions;
using Alcaze.Helper.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alcaze.IC.Typing.Api.Filters
{
    public class JsonExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is LogicException
                || context.Exception is RequiredDocumentTypeException)
            {
                var result = new JsonResult(new { message = context.Exception.Message });
                result.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict;
                context.Result = result;
                return;
            }
            //var jsonResult = new JsonResult(new { error = "Ha ocurrido un problema con la aplicación. Por favor contacte al administrador." });
            var jsonResult = new JsonResult(new { message = context.Exception.ToString() });
            jsonResult.StatusCode= Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
            context.Result = jsonResult;
            //Logger.Error(context.Exception.ToString());

        }
    }
}
