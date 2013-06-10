using Coombu.Exceptions;
using Coombu.Filters;
using Coombu.Models;
using Coombu.Models.Repositories;
using Coombu.Models.ViewModels;
using PagedList;
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
    public class CommentsController : ApiController
    {
        private readonly CoombuContext _db = new CoombuContext();
        private readonly IPostRepository _repository;

        public CommentsController(IPostRepository repo)
        {
            _repository = repo;
        }

        // /posts/{postId}/comments?page=&size=
        [PostNotFoundException]
        public HttpResponseMessage Get(int postId, int page = 1, int size = 25)
        {
            Post post = _db.Posts.Find(postId);
            if (post == null)
            {
                throw new PostNotFoundException(string.Format("Post with id {0} not found ! Cannot retrieve comments for this post", postId));
            }

            IPagedList<Comment> comments = post.Comments.ToPagedList(page, size);
            ApiResponse<List<Comment>> response = new ApiResponse<List<Comment>>(comments.ToList());
            response.AddMetatData("totalPage", comments.PageCount);
            response.AddMetatData("currentPage", comments.PageNumber);
            response.AddMetatData("size", comments.PageSize);

            return Request.CreateResponse<ApiResponse<List<Comment>>>(HttpStatusCode.OK, response);
        }

        // /posts/{postId}/comments/{commentId}

        [PostNotFoundException]
        public HttpResponseMessage Get(int postId, int commentId)
        {
            Post post = _repository.Get(postId);

            Comment comment = post.Comments.First(c => c.CommentId == commentId);

            if (comment == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Comment with id {0} not found", commentId));
            }

            return Request.CreateResponse<ApiResponse<Comment>>(HttpStatusCode.OK, new ApiResponse<Comment>(comment));
        }

        [PostNotFoundException]
        public HttpResponseMessage Post(int postId, Comment comment)
        {
            Post commentedPost = _db.Posts.Find(postId);
            UserProfile commenter = _db.Users.First(u=>u.UserName == HttpContext.Current.User.Identity.Name );

            if (commentedPost == null)
            {
                throw new PostNotFoundException(string.Format("Post with id {0} not found ! Cannot post a comments for this post", postId));
            }

            Comment com = new Comment { Content = comment.Content, User = commenter };
            
            commentedPost.Comments.Add(com);
            _db.Entry<Post>(commentedPost);
            _db.SaveChanges();

            return Request.CreateResponse<ApiResponse<Comment>>(HttpStatusCode.Created, new ApiResponse<Comment>(com));
        }

        [NotFoundException]
        public HttpResponseMessage Delete(int commentId)
        {
            Comment commentToDelete = _db.Comments.Find(commentId);

            if (commentToDelete == null)
            {
                throw new NotFoundException(string.Format("Comment with id {0} not found !", commentId));
            }

            _db.Comments.Remove(commentToDelete);
            _db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
