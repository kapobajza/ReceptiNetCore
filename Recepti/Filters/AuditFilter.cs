using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Recepti.Repository.AuditRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Filters
{
    public class AuditFilter : ActionFilterAttribute
    {
        private readonly IAuditRepo _auditRepo;

        public AuditFilter(IAuditRepo auditRepo)
        {
            _auditRepo = auditRepo;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.ActionDescriptor as ControllerActionDescriptor;
            int.TryParse(context.HttpContext?.User?.FindFirst("Id")?.Value, out int id);
            var actionName = controller?.ActionName;
            var method = context.HttpContext?.Request?.Method;

            var audit = new Models.Audit()
            {
                ActionName = controller?.ActionName,
                ControllerName = controller?.ControllerName,
                IpAddress = context.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                AccessDate = DateTime.Now,
                PageAccessed = context.HttpContext?.Request?.GetDisplayUrl(),
                Method = method,
                KorisnikId = id != 0 ? id : (int?)null,
                LoggedInAt = actionName == "Login" && method == "POST" ? DateTime.Now : (DateTime?)null,
                LoggedOutAt = actionName == "Logout" ? DateTime.Now : (DateTime?)null,
                ResponseStatusCode = context.HttpContext?.Response?.StatusCode ?? 0,
                IsLoggedIn = id != 0 ? true : false
            };

            _auditRepo.Add(audit);
            _auditRepo.SaveChanges();

            base.OnActionExecuting(context);
        }
    }
}
