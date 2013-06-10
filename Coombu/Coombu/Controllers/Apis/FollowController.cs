using Coombu.Filters;
using Coombu.Models;
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
    public class FollowController : ApiController
    {
        private readonly CoombuContext _db = new CoombuContext();

        //POST /api/{id}/follow
        public HttpResponseMessage Post(int id)
        {
            UserProfile current = _db.Users.First(u => u.UserName == HttpContext.Current.User.Identity.Name);
            UserProfile followedUser = _db.Users.Find(id);

            if (followedUser == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Success = false, ErrorMesage = string.Format("Impossible de suivre l'utilisteur avec l'id {0}, utilisateur inexistant", id) });
            }

            current.Followees.Add(followedUser);
            followedUser.Followers.Add(current);

            _db.Entry<UserProfile>(current);
            _db.Entry<UserProfile>(followedUser);
            _db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //DELETE /api/{id}/follow
        public HttpResponseMessage Delete(int id)
        {
            UserProfile current = _db.Users.First(u => u.UserName == HttpContext.Current.User.Identity.Name);
            UserProfile unFollowedUser = _db.Users.Find(id);

            if (unFollowedUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Cannot unfollow user with id {0}, user not found",id));
            }

            current.Followees.Remove(unFollowedUser);
            unFollowedUser.Followers.Remove(current);

            _db.Entry<UserProfile>(current);
            _db.Entry<UserProfile>(unFollowedUser);
            _db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);   
        }
    }
}
