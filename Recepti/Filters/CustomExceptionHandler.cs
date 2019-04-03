using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Filters
{
    public class CustomExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception.InnerException != null && exception.InnerException is SqlException innerException && innerException.Number == 2601)
            {
                context.ModelState.AddModelError("KorisnickoIme", "Korisničko ime već postoji.");
                context.Result = new ViewResult();
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}
