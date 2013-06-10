using CoombuPhoneApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoombuPhoneApp.Converters
{
    public class UserTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() == typeof(UserProfile))
            {
                UserProfile user = (UserProfile)value;
                return string.Format("{0} {1}", user.FirstName, user.LastName);
            }
            else 
            {
                throw new Exception("Convert only a UserProfile object type to Title");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
