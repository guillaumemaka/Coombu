using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
