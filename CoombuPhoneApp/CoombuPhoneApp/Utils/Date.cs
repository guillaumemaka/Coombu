using CoombuPhoneApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.Utils
{
    public class Date
    {
        public static string GetPrettyDate(DateTime d)
        {
            // 1.
            // Get time span elapsed since the date.
            TimeSpan s = DateTime.Now.Subtract(d);

            // 2.
            // Get total number of days elapsed.
            int dayDiff = (int)s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            int secDiff = (int)s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }

            // 5.
            // Handle same-day times.
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return AppResources.Now;
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return AppResources.MinuteAgo;
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format(AppResources.MinutesAgo,
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return AppResources.HourAgo;
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format(AppResources.HoursAgo,
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return AppResources.Yesterday;
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} days ago",
                dayDiff);
            }
            if (dayDiff < 31)
            {
                return string.Format(AppResources.WeeksAgo,
                Math.Ceiling((double)dayDiff / 7));
            }
            return null;
        }
    }
}
