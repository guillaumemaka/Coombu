using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
        public UserProfile User { get; set; }
    }
}
