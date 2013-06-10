using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class UserResultViewModel
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string Department { get; set; }
        public Boolean IsFollowed { get; set; }
        public string UserName { get; set; }
    }
}
