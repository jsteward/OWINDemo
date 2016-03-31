using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Owin;

namespace MiddleWareDemo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule()
        {
            Get["/nancy"] = x =>
            {
                var env = Context.GetOwinEnvironment();
                return "Hello from Nancy! You requested: " + env["owin.RequestPath"];
            };
        }
    }
}