using Recepti.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Account
{
    public class LoginViewModel : GoogleReCaptchaModelBase
    {
        [Required(ErrorMessage = "Polje Korisničko ime je obavezno")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Polje Lozinka je obavezno")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
