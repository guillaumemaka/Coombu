using System;

namespace Coombu.Exceptions
{
    public class PostNotFoundException : Exception
    {        
        public PostNotFoundException(string p) : base(p)
        {                        
        }
        
    }
}
