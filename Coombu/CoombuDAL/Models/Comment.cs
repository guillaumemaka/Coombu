using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coombu.Models
{
    public class Comment
    {
        public Comment()
        {
            CommentedAt = DateTime.Now;
        }

        [Key]
        public int CommentId { get; set; }        
        public string Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CommentedAt { get; set; }
        
        [JsonIgnore]
        public virtual Post Post { get; set; }
        public virtual UserProfile User { get; set; }
    }
}
