using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Recepti
{
    public class KorisnikReceptiViewModel
    {
        public int KorisnikId { get; set; }
        public List<SelectListItem> Kategorije { get; set; }
        public List<ReceptiViewModel> Recepti { get; set; }
    }
}
