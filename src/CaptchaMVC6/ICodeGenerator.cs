using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptchaMVC6
{
    public interface ICodeGenerator
    {
        string Generate(int length);
    }
}
