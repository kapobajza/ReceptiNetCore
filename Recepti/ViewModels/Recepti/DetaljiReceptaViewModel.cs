using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Recepti
{
    public class DetaljiReceptaViewModel
    {
        public int ReceptId { get; set; }
        public string Naziv { get; set; }
        public List<string> Sastav { get; set; }
        public string Priprema { get; set; }
        public string DatumObjave { get; set; }
        public string SlikaURL { get; set; }
        public string Kategorija { get; set; }
        public string Korisnik { get; set; }
    }
}
