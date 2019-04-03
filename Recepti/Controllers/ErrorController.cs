using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Recepti.Repository.ErrorLoggingRepo;

namespace Recepti.Controllers
{
    public class ErrorController : Controller
    {
        private IErrorLoggingRepo _errorLoggingRepo;

        public ErrorController(IErrorLoggingRepo errorLoggingRepo)
        {
            _errorLoggingRepo = errorLoggingRepo;
        }

        public IActionResult Index()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _errorLoggingRepo.Add(new Models.ErrorLogging()
            {
                Date = DateTime.Now,
                Message = exceptionFeature.Error.Message,
                StackTrace = exceptionFeature.Error.StackTrace,
                Path = exceptionFeature.Path
            });
            _errorLoggingRepo.SaveChanges();

            return View("An error occurred. If this error persists, please contact the administrator.");
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var message = $"Error ({statusCode})";

            _errorLoggingRepo.Add(new Models.ErrorLogging()
            {
                Date = DateTime.Now,
                Path = statusCodeData.OriginalPath,
                OriginalBasePath = statusCodeData.OriginalPathBase,
                OriginalQueryString = statusCodeData.OriginalQueryString,
                StatusCode = statusCode
            });
            _errorLoggingRepo.SaveChanges();

            return View("Index", message);
        }
    }
}