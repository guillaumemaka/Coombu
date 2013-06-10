using Coombu.Filters;
using Coombu.Models;
using Coombu.Models.ViewModels;
using Coombu.Utils;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;

namespace Coombu.Controllers.Apis
{    
    [InitializeSimpleMembership]
    public class AuthController : ApiController
    {
        private readonly CoombuContext _db = new CoombuContext();

        [TokenValidation]
        public HttpResponseMessage Get() 
        {
            var token = Request.Headers.First(h => h.Key.Equals("Authorization-token", StringComparison.OrdinalIgnoreCase)).Value.FirstOrDefault();
            UserToken user = _db.UsersToken.Find(token);

            return Request.CreateResponse<UserToken>(HttpStatusCode.OK, user);
        }

        public HttpResponseMessage Post(LoginModel model) 
        {
            HttpResponseMessage response;
            
            if (!WebSecurity.Login(model.UserName, model.Password,persistCookie:model.RememberMe))
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);                
                response.ReasonPhrase = "Unauthorized user";
                return response;
            }
                                
            UserProfile user = _db.Users.First(u => u.UserName.Equals(model.UserName,StringComparison.Ordinal));
            string token = Tokenizer.GenerateToken(user.ToString());

            UserToken usertoken = _db.UsersToken.Find(token);

            if (usertoken == null)
            {
                _db.UsersToken.Add(new UserToken { token = token, User = user });
                _db.SaveChanges();
            }
            
            response = Request.CreateResponse<UserToken>(HttpStatusCode.OK, usertoken);
            response.Headers.Add("Authorization-token",token);
            
            return response;
        }
    }
}
