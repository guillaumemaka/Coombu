using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class PostsIndexViewModel
    {
        public PostViewModelCreate Form { get; set; }
        public PagedPostViewModel PagedPostList { get; set; }
        public List<GroupViewModel> Groups { get; set; }
    }
}
