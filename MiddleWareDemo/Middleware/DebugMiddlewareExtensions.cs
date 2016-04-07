using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using MiddleWareDemo.Middleware;
using Owin;

namespace Owin
{
    public static class DebugMiddlewareExtensions
    {
        public static void UseDebugMiddleware(this IAppBuilder app, DebugMiddlewareOptions options = null)
        {
            if (options == null)
            {
                options = new DebugMiddlewareOptions();
            }

            app.Use<DebugMiddleware>(options);
        }

       
    }

    public class AuthMiddlewareExtensions : OwinMiddleware
    {
        

        public AuthMiddlewareExtensions(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context.Authentication.User.Identity.Name != null &&
                context.Authentication.User.Identity.Name != @"TURNER-NT\jsteward")
            {
                return context.Response.WriteAsync(
                    $"<html><head></head><body>Hello {context.Authentication.User.Identity.Name}</body></html>");
            }
            
            return Next.Invoke(context);
        }
    }
}