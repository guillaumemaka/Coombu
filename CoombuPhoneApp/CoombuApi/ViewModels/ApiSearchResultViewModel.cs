using CoombuPhoneApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.ViewModels
{
    public class ApiSearchResultViewModel
    {
        public string SearchString { get; set; }
        public List<UserProfile> SearchUserResult { get; set; }
        public List<Post> SearchPostResult { get; set; }
    }
}
