using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recepti.Helpers;
using Recepti.ViewModels.Recepti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Filters
{
    public class ModelStateValidate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            var controller = context.Controller as Controller;
            context.ActionArguments.TryGetValue("model", out object model);

            if (model != null && model is DodajIzmijeniReceptViewModel m)
            {
                m.Kategorije = KategorijeRecepta.GetKategorije(m.Kategorija);
            }

            if (!modelState.IsValid)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.Result = controller?.View(model) ?? new ViewResult();
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
