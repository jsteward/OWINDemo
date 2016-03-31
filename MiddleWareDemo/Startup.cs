using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using MiddleWareDemo.Middleware;
using Nancy;
using Owin;
using Nancy.Owin;

namespace MiddleWareDemo
{
    public static class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.UseDebugMiddleware(new DebugMiddlewareOptions
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (ctx) =>
                {
                    var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
                    watch.Stop();
                    Debug.WriteLine("Request took:" + watch.ElapsedMilliseconds + " ms");
                }

            });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Auth/Login")
            });


            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "490383194484704",
                AppSecret = "c05a85b5fcd1c1e2b828b4d641c82e42",
                SignInAsAuthenticationType = "ApplicationCookie"
            });


            app.UseWebApi(config);


            app.Map("/nancy", mappedApp => { mappedApp.UseNancy(); });

            //app.UseNancy(conf =>
            //{
            //    conf.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            //});

            //app.Use(async (ctx, next) =>
            //{
            //    await ctx.Response.WriteAsync("<html><head></head><body>Hello World</body></html>");
            //});
        }
    }
}