using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TestDemo.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //get the current claims principal
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            // get the claims values
            var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                               .Select(c => c.Value).SingleOrDefault();
            var role = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                             .Select(c => c.Value).SingleOrDefault();
           

            if (identity != null && identity.HasClaim(ClaimType, ClaimValue))
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirect or return an unauthorized response based on your application's logic
            filterContext.Result = new HttpUnauthorizedResult();
        }


    }
}