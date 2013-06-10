using Coombu.Filters;
using Coombu.Models;
using Coombu.Models.ViewModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace Coombu.Controllers.Apis
{
    [TokenValidation]
    public class SearchController : ApiController
    {
        CoombuContext db = new CoombuContext();
           
        public HttpResponseMessage Get(String q)
        {
            UserProfile currentUser = db.Users.First(u => u.UserName == HttpContext.Current.User.Identity.Name);
            
            var userPredicate = PredicateBuilder.False<UserProfile>();
            var postPredicate = PredicateBuilder.False<Post>();

            userPredicate = userPredicate.Or(u => u.LastName.Contains(q));
            userPredicate = userPredicate.Or(u => u.FirstName.Contains(q));

            // Exclude current logged user from resulset
            userPredicate = userPredicate.And(u => u.UserName != currentUser.UserName);

            postPredicate.Or(p => p.Content.ToLower().Contains(q.ToLower()));
            
            var users = db.Users.AsExpandable().Where(userPredicate);
            var posts = db.Posts.AsExpandable().Where(postPredicate);

            ApiSearchResultViewModel result = new ApiSearchResultViewModel 
            {
                SearchString = q,
                SearchUserResult = users.ToList(),
                SearchPostResult = posts.ToList()
            };


            return Request.CreateResponse<ApiResponse<ApiSearchResultViewModel>>(HttpStatusCode.OK, new ApiResponse<ApiSearchResultViewModel>(result));
        }
    }

    public class ApiSearchResultViewModel
    {
        public string SearchString { get; set; }
        public List<UserProfile> SearchUserResult { get; set; }
        public List<Post> SearchPostResult { get; set; }
    }
}