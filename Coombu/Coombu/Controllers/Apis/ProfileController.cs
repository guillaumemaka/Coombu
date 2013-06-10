using Coombu.Filters;
using Coombu.Models;
using Coombu.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Coombu.Controllers.Apis
{
    public class ProfileController : ApiController
    {
        private readonly CoombuContext _db = new CoombuContext();
        [TokenValidation]
        public HttpResponseMessage Get() 
        {
            var token = Request.Headers.First(h => h.Key.Equals("Authorization-token", StringComparison.OrdinalIgnoreCase       )).Value.FirstOrDefault();
            UserToken profile = _db.UsersToken.Find(token);
            ApiResponse<UserProfile> response = new ApiResponse<UserProfile>(_db.Users.Find(profile.User.UserId));
            response.AddMetatData("status",HttpStatusCode.OK);

            return Request.CreateResponse<ApiResponse<UserProfile>>(HttpStatusCode.OK, response);
        }
    }
}
