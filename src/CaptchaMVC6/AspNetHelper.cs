using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;

namespace CaptchaMVC6
{
    public static class AspNetHelper
    {
        public static string GetRootAddress(this HttpRequest self)
        {
            return $"{self.Scheme}://{self.Host}";
        }
    }
}
