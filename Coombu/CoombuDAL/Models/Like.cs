using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
