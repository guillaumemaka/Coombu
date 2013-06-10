using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Exceptions
{
    public class UserTokenNotFoundException : Exception
    {
        public UserTokenNotFoundException(string Message) : base(Message) { }
    }
}
