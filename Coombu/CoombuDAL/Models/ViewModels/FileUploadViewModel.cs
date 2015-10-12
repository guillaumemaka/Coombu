using Coombu.Models.Annotations;
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
