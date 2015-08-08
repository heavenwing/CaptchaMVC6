using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptchaMVC6
{
    [TargetElement("div", Attributes = "asp-captcha-control")]
    public class CaptchaControlTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-captcha-control")]
        public string CodeName { get; set; } = "default";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (CaptchaMiddleware.CanOption(CodeName, ViewContext.HttpContext.Session))
                output.SuppressOutput();
        }
    }
}
