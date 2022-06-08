using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PositivePayApi
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> accessKey;
            if (actionContext.Request.Headers.Authorization == null || !actionContext.Request.Headers.TryGetValues("api_key", out accessKey) || !accessKey.Any(x => x == ConfigurationManager.AppSettings["api_key"]))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter));
            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string uname = usernamePasswordArray[0];
            string pass = usernamePasswordArray[1];
            if (uname == ConfigurationManager.AppSettings["user_name"] && pass == ConfigurationManager.AppSettings["password"])
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(uname), null);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}