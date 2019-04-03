using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Recepti
{
    public class DodajIzmijeniReceptViewModel
    {
        public int ReceptId { get; set; }

        [Required(ErrorMessage = "Polje Naziv je obavezno")]
        [StringLength(200, ErrorMessage = "Naziv može sadržavati maksimalno 200 karaktera")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Polje Sastav je obavezno")]
        [Display(Name = "Sastav (molimo da sastojke odvajate zarezom, npr.: jaja, brašno, šećer)")]
        public string Sastav { get; set; }

        [Required(ErrorMessage = "Polje Priprema je obavezno")]
        public string Priprema { get; set; }

        public List<SelectListItem> Kategorije { get; set; }

        [Required(ErrorMessage = "Polje Kategorija je obavezno")]
        public string Kategorija { get; set; }

        public bool Privatan { get; set; }

        [Display(Name = "Odaberite sliku")]
        public IFormFile Slika { get; set; }

        public string SlikaURL { get; set; }
    }
}
