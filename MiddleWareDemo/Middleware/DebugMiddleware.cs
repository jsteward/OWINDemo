﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using AppFunc=System.Func<
    System.Collections.Generic.IDictionary<string,object>, 
    System.Threading.Tasks.Task
    >;

namespace MiddleWareDemo.Middleware
{
    public class DebugMiddleware
    {
        private AppFunc _next;
        private DebugMiddlewareOptions _options;
        public DebugMiddleware(AppFunc next, DebugMiddlewareOptions options)
        {
            _next = next;
            _options = options;

            if (_options.OnIncomingRequest == null)
            {
                _options.OnIncomingRequest = (ctx)=> {
                    Debug.WriteLine("Incoming Request" + ctx.Request.Path);
                };
            }

            if (_options.OnOutgoingRequest == null)
            {
                _options.OnOutgoingRequest = (ctx) => { Debug.WriteLine("Outgoing Request" + ctx.Request.Path); ; };
            }
        }

        public async Task Invoke(IDictionary<string, object> environment )
        {
            var ctx = new OwinContext(environment);
            _options.OnIncomingRequest(ctx);
            await _next(environment);
            _options.OnOutgoingRequest(ctx);
        }
    }
}