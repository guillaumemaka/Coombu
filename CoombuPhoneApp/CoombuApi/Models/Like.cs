using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.Models
{
    public class Like
    {   
        [DataMember]
        public int LikeId { get; set; }        
        [DataMember]
        public UserProfile User { get; set; }
    }
}
