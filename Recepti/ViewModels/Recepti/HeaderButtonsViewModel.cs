using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.ViewModels.Recepti
{
    public class HeaderButtonsViewModel
    {
        public bool IsHomePage { get; set; }
        public List<SelectListItem> Kategorije { get; set; }
    }
}
