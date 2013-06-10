using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.Models
{
    [DataContract]
    public class UserToken
    {

        [DataMember]
        public String token { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public UserProfile User { get; set; }
    }
}
