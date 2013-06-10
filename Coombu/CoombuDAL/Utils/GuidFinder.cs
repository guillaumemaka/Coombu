using Coombu.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Coombu.Utils
{
    public class GuidFinder
    {
        static public String find(string input)
        {
            string pattern = @"[A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}";
            
            Regex re = new Regex(pattern,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches = re.Matches(input);

            if (matches.Count == 0)
            {
                throw new NoGUIDFoundException("No GUID Found !");
            }

            return matches[0].Value;
        }
    }
}
