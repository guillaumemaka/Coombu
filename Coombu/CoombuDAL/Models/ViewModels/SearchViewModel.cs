using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class SearchViewModel
    {
        [Required(ErrorMessage="Veuillez entrer un mot clé pour commencer la recherche")]
        public String q { get; set; }
    }
}
