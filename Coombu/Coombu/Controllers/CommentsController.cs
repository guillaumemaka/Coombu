using Coombu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coombu.Controllers
{
    [Authorize]
    public class CommentsController : BaseController
    {
        public JsonResult Create(string Content, int PostId)
        {
            Post commentedPost = db.Posts.Find(PostId);
            UserProfile commenter = db.Users.Find(GetCurrentUserID());

            if (commentedPost == null)
            {
                return Json(new { Success = false, ErrorMessage = string.Format("Post avvec l'id {0} est introuvable", PostId) });
            }
            Comment comment = new Comment { Content = Content, User = commenter };
            commentedPost.Comments.Add(comment);
            db.Entry<Post>(commentedPost);
            db.SaveChanges();

            return Json(new { Success = true, Comment = new { From = commenter.FirstName + " " + commenter.LastName, Content = comment.Content, PostId = commentedPost.PostId } }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            Comment commentToDelete = db.Comments.Find(id);
            
            if (commentToDelete == null)
            {
                return Json(new { Success = false, ErrorMessage = string.Format("Commentaire avvec l'id {0} est introuvable", id) });
            }

            db.Comments.Remove(commentToDelete);
            db.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
