using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Exceptions
{
    public class NotFoundException : Exception
    {        
        public NotFoundException(string p) : base(p)
        {                        
        }
        
    }
}
