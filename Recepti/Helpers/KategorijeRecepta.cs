using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Helpers
{
    public class KategorijeRecepta
    {
        public static string Deserti = "Deserti";
        public static string HladnaPredjela = "Hladna predjela";
        public static string ToplaPredjela = "Topla predjela";
        public static string GlavnaJela = "Glavna jela";

        public static List<SelectListItem> GetKategorije(string selectedKategorija = "", bool hasDefaultValue = false)
        {
            var list = new List<SelectListItem>();
            var properties = typeof(KategorijeRecepta).GetFields();

            if (hasDefaultValue)
            {
                list.Add(new SelectListItem() { Value = "", Text = "Sve kategorije", Selected = true });
            }

            foreach (var item in properties)
            {
                var value = item.GetValue(item).ToString();
                list.Add(new SelectListItem() { Value = value, Text = value, Selected = value == selectedKategorija });
            }

            return list;
        }
    }
}
