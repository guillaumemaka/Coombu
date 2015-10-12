using System;

namespace Coombu.Exceptions
{
    public class NotFoundException : Exception
    {        
        public NotFoundException(string p) : base(p)
        {                        
        }
        
    }
}
