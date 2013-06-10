using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
