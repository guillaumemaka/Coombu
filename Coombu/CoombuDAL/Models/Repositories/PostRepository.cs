using Coombu.Exceptions;
using System;
using PagedList;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Coombu.Models.Repositories
{
    public class PostRepository : IPostRepository
    {
        private CoombuContext _db { get; set; }

        public PostRepository()
            : this(new CoombuContext()) 
        { 
        }

        public PostRepository(CoombuContext context) 
        {
            _db = context;
        }

        public IEnumerable<Post> GetAll(string username)
        {
            return _db.Users.First(u => u.UserName.Equals(username, StringComparison.Ordinal)).Posts.AsEnumerable();
        }

        public IPagedList<Post> GetAll(string username, int page, int size)
        {
            return _db.Users.First(u => u.UserName.Equals(username,StringComparison.Ordinal)).Posts.ToPagedList(page,size);
        }

        public Post Get(int id)
        {
            Post post = _db.Posts.Find(id);

            if (post == null)
            {
                throw new PostNotFoundException(string.Format("Post with id : {0} not found", id));
            }

            return post;
        }

        public Post Add(Post item)
        {
            Post newPost = _db.Posts.Add(item);
            _db.SaveChanges();
            return newPost;
        }

        public void Remove(int id)
        {
            Post deletedPost = Get(id);
            if (deletedPost == null)
            {                
                throw new PostNotFoundException(string.Format("Post with id : {0} not found",id));
            }

            _db.Posts.Remove(deletedPost);
            _db.SaveChanges();
        }

        public bool Update(Post item)
        {
            _db.Entry<Post>(item);            
            _db.SaveChanges();                            

            return true;
        }       
    }
}
