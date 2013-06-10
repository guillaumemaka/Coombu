using CoombuPhoneApp.Models;
using CoombuPhoneApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.ViewModel
{
    public class PostViewModel
    {
        private string _title = null;
        public string Title 
        {
            get 
            {
                return string.Format("{0} {1}",_title, AppResources.PostTitleStringWrite);
            }

            set
            {
                _title = value;
            }
        }

        public string ContentShort 
        {
            get 
            {
                if (Content.Length > 100)
                {
                    return Content.Substring(0, 100);
                }
                else 
                {
                    return Content;
                }
                
            }
        }

        public int PostId { get; set; }
        public string Content { get; set; }        
        public string Picture { get; set; }
        private DateTime _createdAt;
        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = value; } }

        public String PrettyDate
        {
            get
            {
                return Utils.Date.GetPrettyDate(this._createdAt);
            }            
        }
       
        public int Likes { get; set; }
        public UserProfile User { get; set; }        
        public string LikePlaceHolder 
        {
            get 
            {
                if (Likes > 1)
                {
                    return AppResources.LikeMessagePluralize;
                }
                else 
                {
                    return AppResources.LikeMessageSingular;
                }

            }
        }

        public List<Comment> Comments { get; set; }

        public bool IsLiked { get; set; }
    }
}
