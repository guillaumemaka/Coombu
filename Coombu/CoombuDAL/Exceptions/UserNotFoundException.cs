using System;

namespace Coombu.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string Message) : base(Message) { }
    }
}
