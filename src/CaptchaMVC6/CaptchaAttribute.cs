using Microsoft.AspNet.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Http;

namespace CaptchaMVC6
{
    public class CaptchaAttribute : ValidationAttribute
    {
        public CaptchaAttribute()
        {

        }
        public CaptchaAttribute(string codeName)
        {
            CodeName = codeName;
        }

        public string CodeName { get; set; } = "default";

        /// <summary>
        /// Is option?
        /// </summary>
        public bool CanOption { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContextAccessor = CaptchaMiddleware.ApplicationServices.GetService<IHttpContextAccessor>();
            var session = httpContextAccessor.HttpContext.Session;

            if (CanOption && CaptchaMiddleware.CanOption(CodeName, session))
                return ValidationResult.Success;
            else
            {
                var codeValue = session.GetString(CaptchaMiddleware.SessionKeyPrefix_Value + CodeName);
                return Valid(value, codeValue);
            }
        }

        private static ValidationResult Valid(object value, string codeValue)
        {
            if (value != null)
            {

                if (value.ToString().ToLower().Equals(codeValue.ToLower()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Wrong captcha");
                }
            }
            return new ValidationResult("Captcha must not be blank");
        }
    }
}
