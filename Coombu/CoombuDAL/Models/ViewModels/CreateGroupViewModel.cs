using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class CreateGroupViewModel
    {   
        [Required(ErrorMessage = "Veuillez saisir le nom du groupe à créer !")]
        [Display(Name = "Nom du groupe")]
        public string GroupName { get; set; }
    }
}
