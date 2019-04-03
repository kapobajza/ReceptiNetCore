using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Korisnici
{
    public class KorisnikViewModel
    {
        public int KorisnikId { get; set; }
        public string KorisnickoIme { get; set; }
        public string ImePrezime { get; set; }
        public string Banovan { get; set; }
        public bool BoolBanovan { get; set; }
    }
}
