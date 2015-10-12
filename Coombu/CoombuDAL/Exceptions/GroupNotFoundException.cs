using System;

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
