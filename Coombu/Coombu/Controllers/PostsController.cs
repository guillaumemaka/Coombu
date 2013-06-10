using Coombu.Models;
using Coombu.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using Coombu.Filters;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Coombu.Controllers
{
    [Authorize]    
    public class PostsController : BaseController
    {        
        private Random fileRandom = new Random();
        /*
        public PartialViewResult Create()
        {
            return PartialView("_CreatePostPartial", new PostViewModelCreate());
        } 
        */

        
        CloudStorageAccount accountStorage;
        CloudBlobClient blobClient;
        CloudBlobContainer container;
        CloudQueueClient queueClient;
        CloudQueue queue;
        CloudQueueMessage message;

        public PostsController() 
        {
            accountStorage = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            blobClient = accountStorage.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting("ContainerName"));
            queueClient = accountStorage.CreateCloudQueueClient();
            queue = queueClient.GetQueueReference(CloudConfigurationManager.GetSetting("QueueName"));        
        }

        public ActionResult Index(int? page)
        {
            if (GetCurrentUserID() != -1)
            {
                var user = db.Users.Find(GetCurrentUserID());
                ViewBag.UserFullName = user.FirstName + " " + user.LastName;

                int pageNumber = page ?? 1;

                List<GroupViewModel> groupsViewModel = new List<GroupViewModel>();
                user.Groups.ToList().ForEach(g => groupsViewModel.Add(new GroupViewModel(g, user)));

                PostsIndexViewModel model = new PostsIndexViewModel
                {
                    Form = new PostViewModelCreate(),
                    PagedPostList = GetPosts(pageNumber),
                    Groups = groupsViewModel
                };

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        // Create a post
        //POST /posts
        [HttpPost]        
        public ActionResult Create(PostViewModelCreate model)
        {           
            if (ModelState.IsValid) 
            {
                Post newPost = new Post();

                Trace.WriteLine("Current User Id = " + GetCurrentUserID());

                newPost.Content = model.Content;
                newPost.User = db.Users.Find(GetCurrentUserID());
                    
                if (model.GroupId != -1)
                {
                    Group group = db.Groups.Find(model.GroupId);
                    newPost.Group = group;
                }                
                
                db.Posts.Add(newPost);
                db.SaveChanges();
                
                if (model.File != null)
                {                    
                    CloudQueueMessage message;                    
                    string uniqueBlobName = string.Format("/post-{0}/image_Original_{1}{2}",newPost.PostId, Guid.NewGuid().ToString(), Path.GetExtension(model.File.FileName));
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(uniqueBlobName);
                    blockBlob.Properties.ContentType = model.File.ContentType;
                    blockBlob.UploadFromStream(model.File.InputStream);
                    newPost.Picture = blockBlob.Uri.ToString();
                    
                    message = new CloudQueueMessage(string.Format("{0},{1}",newPost.PostId, newPost.Picture));
                    queue.AddMessage(message);

                    db.Entry<Post>(newPost);
                    db.SaveChanges();
                }
                if (model.GroupId != -1)
                {
                    return RedirectToAction("Detail", "Group", new { id = model.GroupId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }


            List<GroupViewModel> groupsViewModel = new List<GroupViewModel>();
            
            var user = db.Users.Find(GetCurrentUserID());
            user.Groups.ToList().ForEach(g => groupsViewModel.Add(new GroupViewModel(g, user)));

            PostsIndexViewModel indexViewModel = new PostsIndexViewModel 
            {
                Form = model,
                PagedPostList = GetPosts(1),
                Groups = groupsViewModel
            };

            return View("Index",indexViewModel);            
        }

        /*
         *  Delete a post 
         *  DELETE /posts/{id}
         *  return JsonResult {Success:true | false[,ErrorMessage: "...."]}
         */
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            Post postToDelete = db.Posts.Find(id);

            if (postToDelete == null)
            {
                return Json(new { Success = false, ErrorMessage = string.Format("Impossible de supprimer le post avec l'id {0}, le post n'éxiste pas",id) }, JsonRequestBehavior.AllowGet);
            }

            //postToDelete.Likes.Clear();
            //postToDelete.Comments.Clear();

            CloudQueue q = queueClient.GetQueueReference(CloudConfigurationManager.GetSetting("DeletionQueueName"));
            CloudQueueMessage message = new CloudQueueMessage(string.Format("{0},{1}", postToDelete.PostId, postToDelete.Picture));
            q.AddMessage(message);

            db.Posts.Remove(postToDelete);
            db.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        /*
         *  Like a post 
         *  POST /posts/like
         *  return JsonResult {Success:true | false[,ErrorMessage: "...."]}
         */
        [AcceptVerbs("POST","DELETE")]
        public JsonResult Like(int id) 
        {
            Post post = db.Posts.Find(id);
            UserProfile currentUser = db.Users.Find(GetCurrentUserID());
            string message;
            
            // Check if post exist otherwise return NotFound Response
            if (post == null)
            {
                message = string.Format("Post with id: {0}, Not Found",id);
                return Json(new { Success = false, ErrorMessage = message  });
            }

            if (Request.HttpMethod.Equals("POST",StringComparison.OrdinalIgnoreCase)){
                post.Likes.Add(new Like { Post = post, User = currentUser });            
            }else if(Request.HttpMethod.Equals("DELETE",StringComparison.OrdinalIgnoreCase)) {
                var likeToDelete = post.Likes.First(l => l.User.UserId == currentUser.UserId);
                db.Likes.Remove(likeToDelete);
            }else{
                return Json("Unreconized action",JsonRequestBehavior.AllowGet);
            }
     
            db.SaveChanges();

            return Json(new { Success = true, likes = post.Likes.Count},JsonRequestBehavior.AllowGet);
        }

        /*
         *  Disslike a post 
         *  DELETE /posts/like
         *  return JsonResult {Success:true | false[,ErrorMessage: "...."]}
         
        [HttpDelete]
        public JsonResult Like(int id)
        {
            Post disslikedPost = _db.Posts.Find(id);
            UserProfile currentUser = _db.Users.Find(GetCurrentUserID());
            
            string message;
            if (disslikedPost == null)
            {
                message = string.Format("Post with id: {0}, Not Found", id);
                return Json(new { Success = false, ErrorMessage = message });
            }

            var likeToDelete = disslikedPost.Likes.First(l => l.User.UserId == currentUser.UserId);
            _db.Likes.Remove(likeToDelete);
            _db.SaveChanges();

            return Json(new { Success = true, likes = disslikedPost.Likes.Count }, JsonRequestBehavior.AllowGet);
        }
        */


        public JsonResult Page(int id) 
        {          
            return Json(GetPosts(id));
        }

        [NonAction]
        private PagedPostViewModel GetPosts(int page)
        {         
            var posts = db.Users.Find(GetCurrentUserID()).Posts.OrderByDescending(p => p.PostedAt).ToPagedList(page, 25);
            
            object paramsObject = null;
            
            if (posts.HasNextPage)
            {
                paramsObject = new { id = page + 1};    
            }
            
            return new PagedPostViewModel
            {
                Posts = posts.ToList(),
                User = db.Users.Find(GetCurrentUserID()),
                NextPageUrl = (posts.HasNextPage) ? Url.Action("Page", "Posts", paramsObject) : ""
            };
        }
        
    }
}
