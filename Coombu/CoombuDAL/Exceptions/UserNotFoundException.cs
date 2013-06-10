using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string Message) : base(Message) { }
    }
}
