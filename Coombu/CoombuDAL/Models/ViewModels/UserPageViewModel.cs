using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class UserPageViewModel
    {
        public UserPageViewModel() 
        {
            this.Form = new PostViewModelCreate();
        }

        public PostViewModelCreate Form { get; set; }
        public PagedPostViewModel Posts { get; set; }        
        public List<GroupViewModel> Groups { get; set; }
        public UserProfile User { get; set; }        
    }
}
