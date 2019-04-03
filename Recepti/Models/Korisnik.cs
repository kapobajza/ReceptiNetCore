using Recepti.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Models
{
    [Table("Korisnici")]
    public class Korisnik
    {
        public int KorisnikId { get; set; }

        [Required]
        public string KorisnickoIme { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }
        public bool Banovan { get; set; }

        [Required]
        public string Uloga { get; set; }
    }
}
