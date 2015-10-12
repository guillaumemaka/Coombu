using ModelRes;
using System;
using System.ComponentModel.DataAnnotations;
//using System.Web.Security;

namespace Coombu.Models
{
    public class RegisterExternalLoginModel
    {
        [Required(
            ErrorMessageResourceName = "Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [Display(ResourceType = typeof(AccountStrings))]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(
            ErrorMessageResourceName = "Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(AccountStrings))]
        public string OldPassword { get; set; }

        [Required(
            ErrorMessageResourceName = "Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(AccountStrings))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(AccountStrings))]
        [Compare("NewPassword", ErrorMessageResourceName = "PasswordMissMatch2", ErrorMessageResourceType = typeof(ValidationStrings))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(
            ErrorMessageResourceName = "Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [Display(Name="UserName",ResourceType = typeof(AccountStrings))]
        public string UserName { get; set; }

        [Required(
            ErrorMessageResourceName="Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [DataType(DataType.Password)]
        [Display(Name="Password",ResourceType = typeof(AccountStrings))]
        public string Password { get; set; }

        [Display(Name="RememberMe",ResourceType = typeof(AccountStrings))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(
            ErrorMessageResourceName = "Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [Display(ResourceType=typeof(AccountStrings))]
        public string LastName { get; set; }

        [Required(
            ErrorMessageResourceName="Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [Display(ResourceType=typeof(AccountStrings))]
        public string FirstName { get; set; }

        [Required(
            ErrorMessageResourceName="Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [Display(ResourceType=typeof(AccountStrings))]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(
            ErrorMessageResourceName="Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [Display(ResourceType=typeof(AccountStrings))]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(
            ErrorMessageResourceName="Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [Display(ResourceType = typeof(AccountStrings))]
        public string UserName { get; set; }

        [Required(
            ErrorMessageResourceName="Required",
            ErrorMessageResourceType = typeof(ValidationStrings)
            )]
        [StringLength(100, ErrorMessageResourceName="MinLength",ErrorMessageResourceType=typeof(ValidationStrings), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType=typeof(AccountStrings))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType=typeof(ValidationStrings))]
        [Compare("Password", ErrorMessageResourceName="PasswordMissMatch",ErrorMessageResourceType=typeof(ValidationStrings))]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
