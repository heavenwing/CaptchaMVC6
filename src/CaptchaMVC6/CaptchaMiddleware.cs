using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;

namespace CaptchaMVC6
{
    public class CaptchaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICodeGenerator _codeGenerator;
        private readonly IGraphicGenerator _graphicGenerator;

        public CaptchaMiddleware(
            RequestDelegate next,
            ICodeGenerator codeGenerator,
            IGraphicGenerator graphicGenerator)
        {
            _next = next;
            _codeGenerator = codeGenerator;
            _graphicGenerator = graphicGenerator;
        }

        public const string Path = "/captcha";

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            var request = context.Request;

            var path = new PathString(Path);
            if (request.Path == path)
            {
                var codeName = request.Query["name"] ?? "default";
                var length = request.Query["length"];
                var codeLength = string.IsNullOrEmpty(length) ? 4 : int.Parse(length);
                string codeValue = _codeGenerator.Generate(codeLength);
                context.Session.SetString(SessionKeyPrefix_Value + codeName.Trim(), codeValue);
                var buffer = _graphicGenerator.Generate(codeValue);

                context.Response.ContentLength = buffer.Length;
                context.Response.ContentType = "image/jpeg";
                context.Response.StatusCode = 200;
                return context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            return _next.Invoke(context);
        }

        public const string SessionKeyPrefix_Value = "CheckCodeValue_";
        public const string SessionKeyPrefix_Retry = "CheckCodeOption_";

        /// <summary>
        /// because of ValidationAttribute can't get injection
        /// </summary>
        public static IServiceProvider ApplicationServices;

        /// <summary>
        /// 禁用可选状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void DisableOption(string name, ISession session)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (session == null) throw new ArgumentNullException(nameof(session));
            session.SetString(SessionKeyPrefix_Retry + name, "DISABLED");
        }

        /// <summary>
        /// 判断可选状态是否为真
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CanOption(string name, ISession session)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (session == null) throw new ArgumentNullException(nameof(session));
            byte[] value;
            return !session.TryGetValue(SessionKeyPrefix_Retry + name, out value);
        }

        /// <summary>
        /// 清除可选状态的值，以便启用可选判断
        /// </summary>
        /// <param name="name"></param>
        /// <param name="session"></param>
        public static void EnableOption(string name, ISession session)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (session == null) throw new ArgumentNullException(nameof(session));
            session.Remove(SessionKeyPrefix_Retry + name);
        }
    }
}
