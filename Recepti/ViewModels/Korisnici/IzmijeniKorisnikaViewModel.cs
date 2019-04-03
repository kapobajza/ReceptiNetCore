using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Korisnici
{
    public class IzmijeniKorisnikaViewModel
    {
        [Required(ErrorMessage = "Polje Ime je obavezno")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Polje Prezime je obavezno")]
        public string Prezime { get; set; }
    }
}
