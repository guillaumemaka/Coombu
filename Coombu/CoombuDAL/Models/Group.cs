using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coombu.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }        
        public string GroupName { get; set; }    
        public virtual UserProfile Owner { get; set; }
        [JsonIgnore]         
        public virtual ICollection<UserProfile> Users { get; set; }
        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
