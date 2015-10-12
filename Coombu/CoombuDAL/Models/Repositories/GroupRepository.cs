using Coombu.Exceptions;
using PagedList;
using System.Linq;

namespace Coombu.Models.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private CoombuContext _db;        

        public GroupRepository() : this(new CoombuContext()) { }

        public GroupRepository(CoombuContext context)
        {           
            this._db = context;
        }

        public IPagedList<Group> GetAll(int page = 1, int size = 25)
        {
            return _db.Groups.ToPagedList(page,size);
        }

        public IPagedList<Group> GetAll(string username = null, int page = 1, int size = 25)
        {
            IPagedList<Group> groups = null;

            if (username != null)
            {
                UserProfile user = _db.Users.First(u => u.UserName == username);
                if (user == null)
                {
                    throw new UserNotFoundException(string.Format("User with username {0} not found, cannot retrieve groups for this user!", username));
                }

                groups = user.Groups.ToList().ToPagedList(page, size);
            }
            else            
            {
                groups = _db.Groups.ToList().ToPagedList(page, size);
            }

            return groups;                      
        }

        public Group Get(int id)
        {
            Group grp = _db.Groups.Find(id);
            if (grp == null) {
                throw new GroupNotFoundException(string.Format("Group with id : {0} not found", id));
            }

            return grp;
        }

        public Group Add(Group item)
        {
            _db.Groups.Add(item);
            _db.SaveChanges();
            return item;
        }

        public void Remove(int id)
        {
            Group grp = _db.Groups.Find(id);
            if (grp == null)
            {
                throw new GroupNotFoundException(string.Format("Group with id : {0} not found", id));
            }
            _db.Groups.Remove(grp);
            _db.SaveChanges();
        }

        public bool Update(Group item)
        {
            _db.Entry<Group>(item);
            _db.SaveChanges();
            return true;
        }
    }
}
