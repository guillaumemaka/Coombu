using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Coombu.Controllers.Apis
{
    [Authorize]
    public class TestController : ApiController
    {
        [AllowAnonymous]
        public HttpResponseMessage Get() 
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { id = id });
        }
    }
}
