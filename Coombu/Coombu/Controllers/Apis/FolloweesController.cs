using Coombu.Filters;
using Coombu.Models;
using Coombu.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Coombu.Controllers.Apis
{    
    [TokenValidation]
    public class FolloweesController : ApiController
   {
        private readonly CoombuContext _db = new CoombuContext();
        public HttpResponseMessage Get(int id = int.MinValue)
        {
            ApiResponse<List<UserProfile>> response;
            List<UserProfile> data;

            if (id == int.MinValue)
            {
                string currentUsername = HttpContext.Current.User.Identity.Name;
                data = _db.Users.First(u => u.UserName == currentUsername).Followees.ToList();
            }
            else
            {
                data = _db.Users.Find(id).Followees.ToList();
            }
            response = new ApiResponse<List<UserProfile>>(data);

            if (data.Count == 0)
                response.AddMetatData("status", HttpStatusCode.NoContent);
            else
                response.AddMetatData("status", HttpStatusCode.OK);
            return Request.CreateResponse<ApiResponse<List<UserProfile>>>(HttpStatusCode.OK, response);
        }
    }
}
