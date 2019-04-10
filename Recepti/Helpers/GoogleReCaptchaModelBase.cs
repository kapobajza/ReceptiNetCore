using Microsoft.AspNetCore.Mvc;
using Recepti.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Helpers
{
    public abstract class GoogleReCaptchaModelBase
    {
        [GoogleReCaptchaValidation]
        [BindProperty(Name = "g-recaptcha-response")]
        public string GoogleReCaptchaResponse { get; set; }
    }
}
