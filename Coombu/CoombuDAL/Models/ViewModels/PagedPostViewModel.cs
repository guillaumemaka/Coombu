using System;
using System.Collections.Generic;

namespace Coombu.Models.ViewModels
{
    public class PagedPostViewModel
    {
        public List<Post> Posts { get; set; }
        public UserProfile User { get; set; }
        public String NextPageUrl { get; set; }
    }
}
