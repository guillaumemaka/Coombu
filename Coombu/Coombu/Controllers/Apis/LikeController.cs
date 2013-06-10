using Coombu.Exceptions;
using Coombu.Filters;
using Coombu.Models;
using Coombu.Models.Repositories;
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
    public class LikeController : ApiController
    {
        private readonly CoombuContext _db = new CoombuContext();        

        // /api/like/{id}        
        [PostNotFoundException]
        public HttpResponseMessage Post(int id)
        {
            Post post = _db.Posts.Find(id);

            if (post == null)
                throw new PostNotFoundException(string.Format("Post with id : {0} not found", id));

            UserProfile currentUser = _db.Users.First(u => u.UserName == HttpContext.Current.User.Identity.Name);
            string message;
                     
            post.Likes.Add(new Like { Post = post, User = currentUser });
            _db.Entry<Post>(post);
            _db.SaveChanges();
            
            return Request.CreateResponse<ApiResponse<bool>>(HttpStatusCode.OK, new ApiResponse<bool>(true));
        }

        [HttpDelete]        
        public HttpResponseMessage Delete(int id)
        {
            Post post = _db.Posts.Find(id);
            UserProfile currentUser = _db.Users.First(u => u.UserName == HttpContext.Current.User.Identity.Name);
                               
            var likeToDelete = post.Likes.First(l => l.User.UserId == currentUser.UserId);
            _db.Likes.Remove(likeToDelete);           
            _db.SaveChanges();

            return Request.CreateResponse<ApiResponse<bool>>(HttpStatusCode.OK, new ApiResponse<bool>(true));
        }
    }
}
