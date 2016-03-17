using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace MiddleWareDemo.Controllers
{
    [RoutePrefix("api")]
    public class HelloWorldApiController : ApiController
    {
        [Route("hello")]
        [HttpGet]
        public IHttpActionResult HelloWorld()
        {
            return Content(HttpStatusCode.OK, "Hello From Web Api");
        }
    }
}