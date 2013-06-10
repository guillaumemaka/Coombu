using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.Utils
{
    public static class Settings
    {
        public static void Save(string key, object value){
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                IsolatedStorageSettings.ApplicationSettings[key] = value;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(key,value);
            }
        
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static object Get(string key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                return IsolatedStorageSettings.ApplicationSettings[key];
            }
            else
            {
                return null;
            } 
        }
    }
}
