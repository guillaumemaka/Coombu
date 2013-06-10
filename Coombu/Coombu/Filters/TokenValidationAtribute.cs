using Coombu.Exceptions;
using Coombu.Models;
using Coombu.Models.Repositories;
using Microsoft.WindowsAzure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Filters;
using WebMatrix.WebData;

namespace Coombu.Filters
{    
    [InitializeSimpleMembership]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TokenValidationAttribute : ActionFilterAttribute
    {            

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string token;

            try
            {
                token = actionContext.Request.Headers.GetValues("Authorization-Token").First();
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Missing Authorization-Token")
                };
                return;
            }

            try
            {
                UserTokenRepository _repository = new UserTokenRepository();
                UserToken user = _repository.Get(token);                

                var identity = new GenericIdentity(user.User.UserName, "Basic");                

                string[] roles = System.Web.Security.Roles.Provider.GetRolesForUser(user.User.UserName);
                IPrincipal principal = new GenericPrincipal(identity, roles);
                HttpContext.Current.User = principal;
                base.OnActionExecuting(actionContext);
            }
            catch (UserTokenNotFoundException)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Unauthorized User")
                };
                return;
            }
        }
    }
}