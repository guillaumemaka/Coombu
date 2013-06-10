using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace CoombuPhoneApp.Models
{
    [DataContract]
    public class UserProfile
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public System.DateTime DateOfBirth { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string Department { get; set; }        
                
        public override string ToString()
        {
            return string.Format("{0} {1}",                                
                this.FirstName,
                this.LastName                
            );
        }
    }
}
