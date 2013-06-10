using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.Models.ViewModels
{
    public class SearchResultViewModel
    {
        public String SearchString { get; set; }
        public List<UserResultViewModel> SearchUserResult { get; set; }
        public List<Post> SearchPostResult { get; set; }
    }
}
