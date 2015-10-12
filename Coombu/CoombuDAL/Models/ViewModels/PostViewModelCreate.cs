using Coombu.Models.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Coombu.Models.ViewModels
{
    public class PostViewModelCreate
    {
        [Required(ErrorMessage = "Votre status ne peut être vide")]
        [Display(Name = "Status")]        
        [DataType(DataType.MultilineText)]
        public String Content { get; set; }
        
        [FileTypes("jpg,jpeg,png")]
        [Display(Name = "Attacher une image à votre status")]
        public HttpPostedFileBase File { get; set; }
        
        public int GroupId { get; set; }

        public PostViewModelCreate() 
        {
            this.GroupId = -1;
        }
    }
}
