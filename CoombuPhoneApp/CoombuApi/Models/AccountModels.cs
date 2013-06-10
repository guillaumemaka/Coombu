using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
//using System.Web.Security;

namespace Coombu.Models
{
    [DataContract]
    public class LoginModel
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool RememberMe { get; set; }
    }

    [DataContract]
    public class RegisterModel
    {
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public DateTime DateOfBirth { get; set; }
        [DataMember]        
        public string EmailAddress { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}
