using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.Utils
{
    public static class DataStandards
    {
        public static string FormatDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static DateTime FormatDate(string birthDate)
        {
            var parsedDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            return parsedDate;
        }




        //Truncate dates, (not sure if ever gonna need it)
        private static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            //https://stackoverflow.com/questions/1004698/how-to-truncate-milliseconds-off-of-a-net-datetime
            if (timeSpan == TimeSpan.Zero)
                return dateTime; 

            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
    }
}
