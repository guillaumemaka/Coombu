using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.Repositories
{
    public interface IUserTokenRepository
    {
        UserToken Add(UserProfile user);
        UserToken Get(string token);
        void Remove(string token);        
    }
}
