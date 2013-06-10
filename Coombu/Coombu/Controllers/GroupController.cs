using Coombu.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Coombu.Models.ViewModels;
using Coombu.Filters;

namespace Coombu.Controllers
{
    [Authorize]    
    public class GroupController : BaseController
    {

        public ActionResult Index() {            
            UserProfile currentUser = db.Users.Find(GetCurrentUserID());
            List<Group> groups = db.Groups.Include(g => g.Owner).ToList();
            
            List<GroupViewModel> groupsViewModel = new List<GroupViewModel>();
            groups.ForEach(g => groupsViewModel.Add(new GroupViewModel(g, currentUser)));
            
            GroupPageViewModel model = new GroupPageViewModel
            {
                Groups = groupsViewModel,
                CurrentUser = currentUser,
                Form = new CreateGroupViewModel()
            };
            
            return View(model);
        }
        
        public ActionResult Detail(int id)
        {
            Group grp = db.Groups.Find(id);

            if (grp == null)
                return HttpNotFound();

            return View(new DetailGroupViewModel
            {
                CreatePostModel = new PostViewModelCreate { GroupId = grp.GroupId },
                GroupViewModel = new GroupViewModel { Group = grp }
            });
        }

        [HttpPost]
        public ActionResult Create(CreateGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile currentUser = db.Users.Find(GetCurrentUserID());
                Group newGroup = new Group { GroupName = model.GroupName, Owner = currentUser };                                
                             
                currentUser.Groups.Add(newGroup);
                db.Entry<UserProfile>(currentUser);
                db.SaveChanges();

                return RedirectToAction("Detail", new { id = newGroup.GroupId });
            }

            ModelState.AddModelError("","Une erreur est survenue !");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Join(int id)
        {
            Group groupToJoin = db.Groups.Find(id);
            UserProfile user = db.Users.Find(GetCurrentUserID());
            
            if (groupToJoin == null || user == null)
            {
                return Json(new { Success = false, ErrorMessage = string.Format("Le groupe avec l'id : {0} n'existe pas !", id) }, JsonRequestBehavior.AllowGet);
            }

            groupToJoin.Users.Add(user);
            user.Groups.Add(groupToJoin);

            db.Entry<Group>(groupToJoin);
            db.Entry<UserProfile>(user);
            db.SaveChanges();

            return Json(new { Success = true },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Dissjoin(int id)
        {
            Group groupToDissjoin = db.Groups.Find(id);
            UserProfile user = db.Users.Find(GetCurrentUserID());

            if (groupToDissjoin == null || user == null)
            {
                return Json(new { Success = false, ErrorMessage = string.Format("Le groupe avec l'id : {0} n'existe pas !", id) }, JsonRequestBehavior.AllowGet);
            }

            groupToDissjoin.Users.Remove(user);
            db.Entry<Group>(groupToDissjoin);
            db.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List(int page = 1, int size = 25)
        {
            List<Group> groups = db.Groups.ToPagedList(page, size).ToList();
            return View(new PagedGroupViewModel
            {
                Groups = groups,
                NextPageUrl = Url.Action("List", "Group", new { page = page + 1, size = size })
            });
        }

        [HttpPost]
        public ActionResult Post(int groupid, PostViewModelCreate model)
        {
            Group grp = db.Groups.Find(groupid);

            if (ModelState.IsValid)
            {               
                if (grp == null)
                    return HttpNotFound();

                Post newPost = new Post 
                {
                    Content = model.Content
                };

                newPost.Group = grp;

                db.Posts.Add(newPost);
                db.SaveChanges();                
            }

            return View("Index",grp);
        }
    }
}
