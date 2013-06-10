using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException(string Message)
            : base(Message)
        {
        }
    }
}
