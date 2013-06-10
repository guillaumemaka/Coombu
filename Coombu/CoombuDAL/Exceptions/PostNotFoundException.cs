using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Exceptions
{
    public class PostNotFoundException : Exception
    {        
        public PostNotFoundException(string p) : base(p)
        {                        
        }
        
    }
}
