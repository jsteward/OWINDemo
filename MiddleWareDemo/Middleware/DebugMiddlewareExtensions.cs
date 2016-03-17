﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
}