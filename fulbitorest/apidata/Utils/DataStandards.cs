using model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.Utils
{
    public static class DataStandards
    {
        public static string DATE_TIME_FORMAT = "yyyy-MM-dd:HH-mm";
        public static string DATE_FORMAT = "yyyy-MM-dd";

        public static DateTime? DATE_UNDEFINED => null;

        public static string FormatDate(DateTime? date)
        {
            return date?.ToString(DATE_FORMAT);
        }

        public static string FormatDateTime(DateTime? dateTime)
        {
            return dateTime?.ToString(DATE_TIME_FORMAT);
        }

        public static DateTime? FormatDate(string birthDate)
        {
            if (birthDate == null)
                return DataStandards.DATE_UNDEFINED;

            try
            {
                return DateTime.ParseExact(birthDate, DATE_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new UnexpectedInputException("Couldnt parse date format, expected valid gregorian date with format: "+ DATE_FORMAT + ", got: " + birthDate);
            }
        }

        public static DateTime? FormatDateTime(string dateTime)
        {
            if (dateTime == null)
                return DataStandards.DATE_UNDEFINED;

            try
            {
                var date = DateTime.ParseExact(dateTime, DATE_TIME_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
                return date;
            }
            catch (FormatException)
            {
                throw new UnexpectedInputException("Couldnt parse date format, expected valid gregorian date with format: "+ DATE_TIME_FORMAT + ", got: " + dateTime);
            }
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
