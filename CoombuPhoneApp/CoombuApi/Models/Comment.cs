using System;
using System.Runtime.Serialization;

namespace CoombuPhoneApp.Models
{
    [DataContract]
    public class Comment
    {
        public Comment()
        {
            CommentedAt = DateTime.Now;
        }

        [DataMember]
        public int CommentId { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public int PostId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public DateTime CommentedAt { get; set; }                
    }
}
