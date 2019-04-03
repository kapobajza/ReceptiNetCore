using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Models
{
    [Table("Recepti")]
    public class Recept
    {
        public int ReceptId { get; set; }

        [Required]
        [StringLength(200)]
        public string Naziv { get; set; }

        [Required]
        public string Sastav { get; set; }

        [Required]
        public string Priprema { get; set; }

        [Required]
        public string Kategorija { get; set; }

        [Required]
        public DateTime DatumObjave { get; set; }

        [Required]
        public string SlikaURL { get; set; }
        public bool Privatan { get; set; }

        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
