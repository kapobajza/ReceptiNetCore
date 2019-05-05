using Recepti.Filters;
using Recepti.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Account
{
    public class RegisterViewModel /*: GoogleReCaptchaModelBase*/
    {
        [Required(ErrorMessage = "Polje Korisničko ime je obavezno")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Polje Lozinka je obavezno")]
        [PasswordValidator(ErrorMessage = "Lozinka mora sadržavati od 8 do 25 karaktera, bar jedno malo i veliko slovo, bar jednu cifru i bar jedan od specijalnih karaktera -_#$")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Polje Ponovite lozinku je obavezno")]
        [PasswordValidator(ErrorMessage = "Lozinka mora sadržavati od 8 do 25 karaktera, bar jedno malo i veliko slovo, bar jednu cifru i bar jedan od specijalnih karaktera -_#$")]
        [Display(Name = "Ponovite lozinku")]
        [DataType(DataType.Password)]
        [Compare("Lozinka", ErrorMessage = "Lozinke moraju biti jednake")]
        public string LozinkaPonovo { get; set; }

        [Required(ErrorMessage = "Polje Ime je obavezno")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Polje Prezime je obavezno")]
        public string Prezime { get; set; }
    }
}
