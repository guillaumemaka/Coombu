using System.Collections.Generic;

namespace Coombu.Models.ViewModels
{
    public class GroupPageViewModel
    {
        public CreateGroupViewModel Form { get; set; }
        public List<GroupViewModel> Groups { get; set; }
        public UserProfile CurrentUser { get; set; }
    }
}
