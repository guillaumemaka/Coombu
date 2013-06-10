using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Web;

namespace Coombu.Models
{
    public class Post
    {
        public Post()
        {
            PostedAt = DateTime.Now;            
        }        

        [Key]
        public int PostId { get; set; }
        
        [Required]        
        public string Content { get; set; }
        public string Picture { get; set; }
        
        public int UserId { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime PostedAt { get; set; }
                
        public virtual UserProfile User { get; set; }
                
        public virtual Group Group { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
                
        public virtual ICollection<Like> Likes { get; set; }

        [NotMapped]        
        private bool _isLiked;

        [NotMapped]
        [JsonProperty]
        public bool IsLiked 
        { 
            get 
            {
                Like like;
                try
                {
                    like = (from l in Likes
                                 where l.User.UserName == HttpContext.Current.User.Identity.Name && l.Post == this
                                 select l).FirstOrDefault() as Like;
                }
                catch
                {

                    like = null;
                }

                if (like != null)
                    return true;

                return false;                                 
            }
            set
            {
                _isLiked = value;
            }
        }        
    }
}
