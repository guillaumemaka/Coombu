using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coombu.Models;
using System.Web.Http;
using Coombu.Models.ViewModels;
using System.Net.Http;
using Coombu.Models.Repositories;
using System.Net;
using Coombu.Filters;

namespace Coombu.Controllers.Apis
{
    [TokenValidation]
    public class PostsController : ApiController
    {
        private readonly IPostRepository _repository;
        private readonly CoombuContext _db = new CoombuContext();
        public PostsController(IPostRepository repo) 
        {
            _repository = repo;
        }

        //
        // GET: /api/posts/?username=&page=&sise=
        
        public HttpResponseMessage Get(string username = null, int page = 1, int size = 25 )
        {
            if (username == null) 
            {
                username = HttpContext.Current.User.Identity.Name;
            }

            var posts = _repository.GetAll(username, page, size);

            ApiResponse<List<Post>> response;
            
            if (posts == null)
            {
                response = new ApiResponse<List<Post>>(new List<Post>());
                response.AddMetatData("totalPage", 0);
                response.AddMetatData("currentPage", 0);
                response.AddMetatData("size", size);

                return Request.CreateResponse<ApiResponse<List<Post>>>(HttpStatusCode.OK, response);            
            }
            else
            {
                response = new ApiResponse<List<Post>>(posts.ToList());
                response.AddMetatData("totalPage", posts.PageCount);
                response.AddMetatData("currentPage", posts.PageNumber);
                response.AddMetatData("size", posts.PageSize);

                return Request.CreateResponse<ApiResponse<List<Post>>>(HttpStatusCode.OK, response);            
            }                    
        }

        [PostNotFoundException]
        public HttpResponseMessage Get(int id) 
        {
            Post post = _repository.Get(id);            
            return Request.CreateResponse<ApiResponse<Post>>(HttpStatusCode.OK, new ApiResponse<Post>(post));
        }

        public HttpResponseMessage Post(Post post)
        {
            ApiResponse<Post> response;

            if (!ModelState.IsValid)
            {
                response = new ApiResponse<Post>();                
                response.AddMetatData("Errors", ModelState.AsEnumerable());
                return Request.CreateResponse<ApiResponse<Post>>(HttpStatusCode.BadRequest, response);
            }

            UserProfile current = _db.Users.First(u => u.UserName == HttpContext.Current.User.Identity.Name);
            Post newPost = new Post { Content = post.Content, User = current };
            _db.Posts.Add(newPost);
            _db.SaveChanges();

            response = new ApiResponse<Post>(newPost);
                           
            return Request.CreateResponse<ApiResponse<Post>>(HttpStatusCode.Created, response);
        }
        
        [PostNotFoundException]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            _repository.Remove(id);
            ApiResponse<bool>response = new ApiResponse<bool>(true);            
            return Request.CreateResponse<ApiResponse<bool>>(HttpStatusCode.OK, response);
        }
    }
}