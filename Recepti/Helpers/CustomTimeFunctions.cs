using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Helpers
{
    public class CustomTimeFunctions
    {
        public static string TimeAgo(DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
            {
                var sekunde = ts.Seconds < 5 ? "sekunde" : "sekundi";
                return ts.Seconds == 1 ? "prije jedne sekunde" : $"prije {ts.Seconds} {sekunde}";
            }

            if (delta < 2 * MINUTE)
                return "prije jedne minute";

            if (delta < 45 * MINUTE)
            {
                var minute = ts.Minutes < 5 ? "minute" : "minuta";
                return $"prije {ts.Minutes} {minute}";
            }

            if (delta < 90 * MINUTE)
                return "prije jednog sata";

            if (delta <= 20 * HOUR)
            {
                var sati = ts.Hours < 5 ? "sata" : "sati";
                return $"prije {ts.Hours} {sati}";
            }

            if (delta > 20 * HOUR && delta < 24 * HOUR)
                return "jučer";

            if (delta < 30 * DAY)
                return $"prije {ts.Days} dana";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                var mjeseci = months < 5 ? "mjeseca" : "mjeseci";
                return months <= 1 ? "prije jednog mjeseca" : $"prije {months} {mjeseci}";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                var godina = years < 5 ? "godine" : "godina";
                return years <= 1 ? "prije jedne godine" : $"prije {years} {godina}";
            }
        }        
    }
}
