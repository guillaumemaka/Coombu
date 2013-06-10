using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.Utils
{
    public class NoParalaxTitleLayer : PanningTitleLayer
    {
        protected override double PanRate
        {
            get { return 1d; }
        }
    }
}
