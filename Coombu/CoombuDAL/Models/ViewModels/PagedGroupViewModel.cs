using System.Collections.Generic;

namespace Coombu.Models.ViewModels
{
    public class PagedGroupViewModel
    {
        public List<Group> Groups { get; set; }
        public string NextPageUrl { get; set; }
    }
}
