using CaptchaMVC6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Builder
{
    public static class CaptchaMiddlewareExtensions
    {
        public static IApplicationBuilder UseCaptcha(this IApplicationBuilder builder)
        {
            CaptchaMiddleware.ApplicationServices = builder.ApplicationServices;
            return builder.UseMiddleware<CaptchaMiddleware>();
        }
    }
}
