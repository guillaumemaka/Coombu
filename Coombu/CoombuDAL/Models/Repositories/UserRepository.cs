using Coombu.Exceptions;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.Repositories
{
    public class UserRepository :IUserRepository
    {
        private CoombuContext _db { get; set; }

        public UserRepository() : this(new CoombuContext()) { }

        public UserRepository(CoombuContext context)
        {
            _db = context;
        }

        public IPagedList<UserProfile> GetAll(int page = 1, int size = 25) 
        {
            return _db.Users.ToPagedList(page,size);
        }

        public UserProfile Get(string username)
        {
            UserProfile user = _db.Users.First(u => u.UserName == username);

            if (user == null)
            {
                throw new UserNotFoundException(string.Format("User with username {0} not found",username));
            }

            return user;
        }

        public UserProfile Get(int id)
        {
            UserProfile user = _db.Users.Find(id);

            if (user == null)
            {
                throw new UserNotFoundException(string.Format("User with userid {0} not found", id));
            }

            return user;
        }

        public UserProfile Add(UserProfile user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public UserProfile Update(UserProfile user)
        {
            if (_db.Entry(user).State == EntityState.Modified)
            {
                _db.SaveChanges(); 
            }
            return user;
        }

        public void Remove(int id)
        {
            UserProfile user = _db.Users.Find(id);
            if(user == null)
            {
                throw new UserNotFoundException(string.Format("User with userid {0} not found", id));
            }

            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}
