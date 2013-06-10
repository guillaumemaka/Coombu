using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.Repositories
{
    public interface IGroupRepository
    {        
        IPagedList<Group> GetAll(int page = 1, int size = 25);
        IPagedList<Group> GetAll(string username, int page = 1, int size = 25);
        Group Get(int id);
        Group Add(Group item);
        void Remove(int id);
        bool Update(Group item);        
    }
}
