using System;

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
