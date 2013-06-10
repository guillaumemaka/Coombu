using Coombu.Models.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Coombu.Models.ViewModels
{
    public class FileUploadViewModel
    {
        [FileTypes("jpg,png,jpeg,tif")]
        [FileSize(4096)]
        public HttpPostedFile File { get; set; }
    }
}
