using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coombu.Models.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public Boolean IsLiked { get; set; }
    }
}
