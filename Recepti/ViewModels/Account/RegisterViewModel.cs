using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Polje Korisničko ime je obavezno")]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Polje Lozinka je obavezno")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Polje Ponovite lozinku je obavezno")]
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
