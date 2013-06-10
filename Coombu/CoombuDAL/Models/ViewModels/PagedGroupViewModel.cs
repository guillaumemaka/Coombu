using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class PagedGroupViewModel
    {
        public List<Group> Groups { get; set; }
        public string NextPageUrl { get; set; }
    }
}
