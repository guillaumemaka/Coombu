using System;

namespace Coombu.Exceptions
{
    public class UserTokenNotFoundException : Exception
    {
        public UserTokenNotFoundException(string Message) : base(Message) { }
    }
}
