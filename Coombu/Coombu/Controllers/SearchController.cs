using Coombu.Filters;
using Coombu.Models;
using Coombu.Models.ViewModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Coombu.Controllers
{
    [Authorize]    
    public class SearchController : BaseController
    {
        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            MembershipUser user = Membership.GetUser();
            UserProfile currentUser = db.Users.Find(int.Parse(user.ProviderUserKey.ToString()));                        

            var userPredicate = PredicateBuilder.False<UserProfile>();
            var postPredicate = PredicateBuilder.False<Post>();
      
            userPredicate = userPredicate.Or(u => u.LastName.Contains(model.q));
            userPredicate = userPredicate.Or(u => u.FirstName.Contains(model.q));
            
            // Exclude current logged user from resulset
            userPredicate = userPredicate.And(u => u.UserName != user.UserName);

            postPredicate.Or(p => p.Content.ToUpper().Contains(model.q.ToUpper()));

            var users = db.Users.AsExpandable().Where(userPredicate);
            var posts = db.Posts.AsExpandable().Where(postPredicate);

            SearchResultViewModel result = new SearchResultViewModel
            {
                SearchString = model.q,
                SearchPostResult = posts.ToList()
            };

            List<UserResultViewModel> usersResult = new List<UserResultViewModel>();

            foreach (UserProfile u in users)
            {
                Boolean followed = u.Followers.Contains(currentUser);
                usersResult.Add(new UserResultViewModel
                {
                    UserId = u.UserId,
                    UserFullName = string.Format("{0} {1}", u.FirstName, u.LastName),
                    Department = u.Department,
                    IsFollowed = followed
                });
            }

            result.SearchUserResult = usersResult;
            return View(result);                        
        }

        public ActionResult SearchPartialView() {
            return PartialView("_SearchPartial", new SearchViewModel());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
