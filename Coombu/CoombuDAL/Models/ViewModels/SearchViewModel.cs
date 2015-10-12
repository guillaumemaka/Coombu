using System;
using System.ComponentModel.DataAnnotations;

namespace Coombu.Models.ViewModels
{
    public class SearchViewModel
    {
        [Required(ErrorMessage="Veuillez entrer un mot clé pour commencer la recherche")]
        public String q { get; set; }
    }
}
