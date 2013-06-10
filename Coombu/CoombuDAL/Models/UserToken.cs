using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
