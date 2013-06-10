using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class GroupPageViewModel
    {
        public CreateGroupViewModel Form { get; set; }
        public List<GroupViewModel> Groups { get; set; }
        public UserProfile CurrentUser { get; set; }
    }
}
