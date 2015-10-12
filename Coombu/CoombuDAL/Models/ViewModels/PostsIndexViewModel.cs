using System.Collections.Generic;

namespace Coombu.Models.ViewModels
{
    public class PostsIndexViewModel
    {
        public PostViewModelCreate Form { get; set; }
        public PagedPostViewModel PagedPostList { get; set; }
        public List<GroupViewModel> Groups { get; set; }
    }
}
