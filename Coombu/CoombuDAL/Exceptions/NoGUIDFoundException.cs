using System;

namespace Coombu.Exceptions
{
    public class NoGUIDFoundException : Exception
    {
        private string p;

        public NoGUIDFoundException(string p) : base(p)
        {
            
        }
    }
}
