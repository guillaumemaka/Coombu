using System.ComponentModel.DataAnnotations;

namespace Coombu.Models.ViewModels
{
    public class CreateGroupViewModel
    {   
        [Required(ErrorMessage = "Veuillez saisir le nom du groupe à créer !")]
        [Display(Name = "Nom du groupe")]
        public string GroupName { get; set; }
    }
}
