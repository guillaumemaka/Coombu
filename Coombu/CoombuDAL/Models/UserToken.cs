using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coombu.Models
{
    public class UserToken
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public String token {get; set;}
        public int UserId { get; set; }
        public virtual UserProfile User { get; set; }
    }
}
