using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Web.Security;

namespace Coombu.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }                
        public string UserName { get; set; }                
        public string FirstName { get; set; }                
        public string LastName { get; set; }                
        public System.DateTime DateOfBirth { get; set; }                
        public string EmailAddress { get; set; }                
        public string Department { get; set; } 
        
        [JsonIgnore]
        public virtual ICollection<Group> Groups { get; set; }
        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserProfile> Followers { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserProfile> Followees { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}",
                this.UserId,
                this.UserName,
                this.FirstName,
                this.LastName,
                this.DateOfBirth.ToString(),
                this.EmailAddress,
                this.Department);
        }
    }
}
