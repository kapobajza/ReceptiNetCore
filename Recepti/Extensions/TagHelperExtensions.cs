using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Extensions
{
    public static class TagHelperExtensions
    {
        public static IHtmlContent GoogleReCaptcha(this IHtmlHelper htmlHelper, string siteKey, string callback = null)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.Attributes.Add("class", "g-recaptcha");
            tagBuilder.Attributes.Add("data-sitekey", siteKey);

            if (callback != null && string.IsNullOrWhiteSpace(callback))
            {
                tagBuilder.Attributes.Add("data-callback", callback);
            }

            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                var htmlOutput = writer.ToString();
                return htmlHelper.Raw(htmlOutput);
            }
        }
    }
}
