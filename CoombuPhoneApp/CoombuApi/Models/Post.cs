using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CoombuPhoneApp.Models
{       
    public class Post
    {
        public Post()
        {
            PostedAt = DateTime.Now;            
        }

        [DataMember]
        public int PostId { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string Picture { get; set; }
        [DataMember]
        public UserProfile User { get; set; }
        [DataMember]
        public List<Comment> Comments { get; set; }
        [DataMember]
        public DateTime PostedAt { get; set; }        
        [DataMember]
        public Boolean IsLiked { get; set; }
        [DataMember]
        public List<Like> Likes { get; set; }                
    }
}
