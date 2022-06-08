using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace PositivePayApi.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            string userName = Thread.CurrentPrincipal.Identity.Name;
            if (userName == "")
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            return Request.CreateResponse(HttpStatusCode.OK, new { Employees = new List<object> { new { Name = "Test1", Age = 25 }, new { Name = "Test 2", Age = 30 } } });
        }
    }
}
