using CoombuPhoneApp.Models;
using System.Collections.Generic;

namespace CoombuPhoneApp.Models
{
    public class Group
    {        
        public int GroupId { get; set; }        
        public string GroupName { get; set; }    
        public virtual UserProfile Owner { get; set; }                    
        public virtual ICollection<UserProfile> Users { get; set; }         
        public virtual ICollection<Post> Posts { get; set; }
    }
}
