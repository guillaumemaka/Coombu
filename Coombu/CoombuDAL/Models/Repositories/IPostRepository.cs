using PagedList;
using System.Collections.Generic;

namespace Coombu.Models.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAll(string username);
        IPagedList<Post> GetAll(string username, int page, int size);
        Post Get(int id);
        Post Add(Post item);
        void Remove(int id);
        bool Update(Post item);
    }
}
