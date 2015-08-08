using CaptchaMVC6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Framework.DependencyInjection
{
    public static class CaptchaServiceCollectionExtensions
    {
        public static IServiceCollection AddCaptcha(this IServiceCollection services)
        {
            services.AddTransient<ICodeGenerator, DefaultCodeGenerator>();
            services.AddTransient<IGraphicGenerator, DefaultGraphicGenerator>();
            return services;
        }

        public static IServiceCollection AddCaptcha<TCode, TGraphic>(this IServiceCollection services)
            where TCode : class, ICodeGenerator
            where TGraphic : class, IGraphicGenerator
        {
            services.AddTransient<ICodeGenerator, TCode>();
            services.AddTransient<IGraphicGenerator, TGraphic>();
            return services;
        }
    }
}
