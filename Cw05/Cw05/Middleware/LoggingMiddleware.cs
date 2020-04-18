using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cw05.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            if(context.Request != null)
            {
                string path = context.Request.Path;
                string method = context.Request.Method;
                string queryString = context.Request.QueryString.ToString();
                string bodyStr = "";
                using(var reader = new StreamReader(context.Request.Body,Encoding.UTF8,true,1024,true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                string logPath = "Log.txt";
                if (!File.Exists(logPath))
                {
                    File.Create(logPath).Dispose();
                }
                StreamWriter sw = File.AppendText(logPath);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(path);
                sw.WriteLine(method);
                sw.WriteLine(queryString);
                sw.WriteLine(bodyStr);
                sw.WriteLine("---------------");
                sw.Close();
            }

            if (_next!=null) await _next(context);
        }
    }
}
