using Coombu.Filters;
using Coombu.Models;
using Coombu.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Security;

namespace Coombu.Controllers
{
    [Authorize]    
    public class UsersController : BaseController
    {
        
        // 
        // GET: /users/{id}/?page=<pagenumber>
        public ActionResult Index(int id, int page = 1 )
        {
            UserProfile user = db.Users.Find(id);

            ViewBag.UserFullName = string.Format("{0} {1}", user.FirstName, user.LastName);
            ViewBag.UserName = user.UserName;
            
            List<GroupViewModel> groupsViewModel = new List<GroupViewModel>();                
            user.Groups.ToList().ForEach(g => groupsViewModel.Add(new GroupViewModel(g, user)));

            return View(new UserPageViewModel
            {                
                Posts = GetPostsForUser(user, page),
                Groups = groupsViewModel,
                User = user
            });
        }

        public JsonResult Follow(int id) 
        {
            UserProfile current = db.Users.Find(GetCurrentUserID());
            UserProfile followedUser = db.Users.Find(id);

            if (followedUser == null)
            {
                return Json( new { Success = false, ErrorMesage = string.Format("Impossible de suivre l'utilisteur avec l'id {0}, utilisateur inexistant") },JsonRequestBehavior.AllowGet);
            }

            current.Followees.Add( followedUser );
            followedUser.Followers.Add( current );

            db.Entry<UserProfile>(current);
            db.Entry<UserProfile>(followedUser); 
            db.SaveChanges();

            return Json(new { Success = true },JsonRequestBehavior.AllowGet);
        }

        public JsonResult Unfollow(int id)
        {
            UserProfile current = db.Users.Find(GetCurrentUserID());
            UserProfile unFollowedUser = db.Users.Find(id);

            if (unFollowedUser == null)
            {
                return Json(new { Success = false, ErrorMesage = string.Format("Impossible de ne plus suivre l'utilisteur avec l'id {0}, utilisateur inexistant") }, JsonRequestBehavior.AllowGet);
            }

            current.Followees.Remove(unFollowedUser);
            unFollowedUser.Followers.Remove(current);

            db.Entry<UserProfile>(current);
            db.Entry<UserProfile>(unFollowedUser);
            db.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        private PagedPostViewModel GetPostsForUser(UserProfile user, int page)
        {   
            var pagedPost = user.Posts.ToPagedList(page,25);
            return new PagedPostViewModel
            {
                Posts = pagedPost.ToList(),
                User = db.Users.Find(GetCurrentUserID()),
                NextPageUrl = Url.Action("Index", "Users", new { id = user.UserId, page = page + 1 })
            };
        }

        [NonAction]
        private int GetCurrentUserID()
        {
            MembershipUser user = Membership.GetUser();

            if (user == null)
                return -1;

            return int.Parse(user.ProviderUserKey.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
