using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Coombu.Models.Annotations
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            HttpPostedFileWrapper file = (HttpPostedFileWrapper) value;
            
            if (value == null) return true;

            return _maxSize > file.ContentLength ;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Le fichier ne doit pas excédé {0} Mo", _maxSize/1024);
        }
    }
}
