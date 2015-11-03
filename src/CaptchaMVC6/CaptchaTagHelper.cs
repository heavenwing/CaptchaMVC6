using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Mvc.Rendering;

namespace CaptchaMVC6
{
    [HtmlTargetElement("img", Attributes = "asp-captcha")]
    public class CaptchaTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-captcha")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// deal with different page base of name
        /// </summary>
        [HtmlAttributeName("asp-captcha-name")]
        public string CodeName { get; set; } = "default";

        [HtmlAttributeName("asp-captcha-length")]
        public int? CodeLength { get; set; } = 4;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Enabled.HasValue && Enabled.Value)
            {
                var src = $"{ViewContext.HttpContext.Request.GetRootAddress()}{CaptchaMiddleware.Path}?name={CodeName}&length={CodeLength}";
                output.Attributes["src"] = src;
                output.Attributes["onclick"] = $"javascript: $(this).attr('src', '{src}&timestamp='+(new Date()).getTime())";
            }
        }
    }
}
