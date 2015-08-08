using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptchaMVC6
{
    public class DefaultCodeGenerator : ICodeGenerator
    {
        public string Generate(int length)
        {
            string s = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string str = "";
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                str += s.Substring(r.Next(s.Length), 1);
            }
            return str;
        }
    }
}
