using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Korisnici
{
    public class PromijeniLozinkuViewModel
    {
        [Required(ErrorMessage = "Polje Trenutna lozinka je obavezno")]
        [Display(Name = "Trenutna lozinka")]
        [DataType(DataType.Password)]
        public string TrenutnaLozinka { get; set; }

        [Required(ErrorMessage = "Polje Nova lozinka je obavezno")]
        [Display(Name = "Nova lozinka")]
        [Compare("NovaLozinkaPonovo", ErrorMessage = "Lozinke moraju biti jednake")]
        [DataType(DataType.Password)]
        public string NovaLozinka { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [Display(Name = "Ponovite novu lozinku")]
        [Compare("NovaLozinka", ErrorMessage = "Lozinke moraju biti jednake")]
        [DataType(DataType.Password)]
        public string NovaLozinkaPonovo { get; set; }
    }
}
