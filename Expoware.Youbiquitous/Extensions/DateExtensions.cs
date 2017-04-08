///////////////////////////////////////////////////////////////////
//
// Youbiquitous
// Author: Dino Esposito
//

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Expoware.Youbiquitous.Extensions
{
    public static class DateExtensions
    {
        public static string ToStringOrEmpty(this DateTime theDate, string format, string empty = "")
        {
            if (theDate == DateTime.MinValue || theDate == DateTime.MaxValue)
                return empty;

            return theDate.ToString(format);
        }

        public static string ToStringOrEmpty(this DateTime? theDate, string format, string empty = "")
        {
            return theDate.GetValueOrDefault().ToStringOrEmpty(format);
        }

        public static string ToYearsOrEmpty(this DateTime theDate, string empty = "")
        {
            if (theDate == DateTime.MinValue)
                return empty;

            return ComputeYears(theDate).ToString();
        }

        public static string ToYearsOrEmpty(this DateTime? theDate, string empty = "")
        {
            return theDate.GetValueOrDefault().ToYearsOrEmpty(empty);
        }

        public static string ToAge(this DateTime theDate, string empty = "")
        {
            if (theDate == DateTime.MinValue)
                return empty;

            return String.Format("{0} {1}", ComputeYears(theDate), Strings_Core.Date_Years);
        }

        public static string ToAge(this DateTime? theDate, string empty = "")
        {
            return theDate.GetValueOrDefault().ToAge(empty);
        }

        public static string Format(this DateTime? theDate, string format, string empty = "")
        {
            if (theDate == DateTime.MinValue || !theDate.HasValue)
                return empty;

            return theDate.Value.ToString(format);
        }

        public static DateTime? Timezone(this DateTime? theDate, double gmtOffset = 0)
        {
            if (theDate == DateTime.MinValue || !theDate.HasValue || gmtOffset.Equals(0))
                return theDate;

            var mins = gmtOffset * 60;
            return theDate.Value.AddMinutes(mins);
        }

        public static string HumanizeElapsedTime(this DateTime theDate, string format = "{0}", string empty = "")
        {
            if (theDate == DateTime.MinValue)
                return empty;

            // Timespan
            var ts = DateTime.UtcNow.Date - theDate;
            var days = ts.TotalDays;
            if (days < 0)
                return empty;

            if (days < 1 && DateTime.Today == theDate.Date)
                return String.Format(format, Strings_Core.Date_Today).ToLower();
            if (days < 2)
                return String.Format(format, Strings_Core.Date_Yesterday).ToLower();
            if (days < 8)
                return String.Format(format, Strings_Core.Date_LastWeek).ToLower();
            if (days < 18)
                return String.Format(format, Strings_Core.Date_AboutTwoWeeksAgo).ToLower();
            if (days < 50)
                return String.Format(format, Strings_Core.Date_AboutOneMonthAgo).ToLower();
            var months = days / 30;

            //var result = String.Format("{0} {1}", (int) months, Strings_Core.DaysAgo);
            var result = String.Format("{0} {1}", (int)months, Strings_Core.Date_MonthsAgo);
            return String.Format(format, result);
        }

        public static string HumanizeElapsedTime(this DateTime? theDate, string format = "{0}", string empty = "")
        {
            if (theDate == DateTime.MinValue || !theDate.HasValue)
                return empty;

            return theDate.Value.HumanizeElapsedTime(format, empty);
        }

        public static string HumanizeForwardTime(this DateTime theDate, string format = "{0}", string empty = "")
        {
            if (theDate == DateTime.MaxValue)
                return empty;

            // Timespan
            var ts = theDate.Date - DateTime.Today;
            var days = ts.TotalDays;
            if (days < 0)
                return String.Format(format, Strings_Core.Date_Expired).ToLower();
            if (days < 1 && DateTime.Today == theDate.Date)
                return String.Format(format, Strings_Core.Date_Today).ToLower();
            if (days < 2)
                return String.Format(format, Strings_Core.Date_Tomorrow).ToLower();
            if (days < 8)
                return String.Format(format, Strings_Core.Date_NextWeek).ToLower();
            if (days < 18)
                return String.Format(format, Strings_Core.Date_InAboutTwoWeeks).ToLower();
            if (days < 50)
                return String.Format(format, Strings_Core.Date_InAboutOneMonth).ToLower();
            var months = days / 30;

            //var result = String.Format("{0} {1}", (int) months, Strings_Core.DaysAgo);
            var result = String.Format("{0} {1}", (int)months, Strings_Core.Date_InMonths);
            return String.Format(format, result);
        }

        public static string HumanizeForwardTime(this DateTime? theDate, string format = "{0}", string empty = "")
        {
            if (theDate == DateTime.MaxValue || !theDate.HasValue)
                return empty;

            return theDate.Value.HumanizeForwardTime(format, empty);
        }

        public static string HumanizeDistance(this DateTime theDate)
        {
            var elapsed = HumanizeElapsedTime(theDate);
            if (elapsed.IsNullOrWhitespace())
                return HumanizeForwardTime(theDate);
            return elapsed;
        }

        public static string HumanizeDistance(this DateTime? theDate)
        {
            if (theDate.HasValue)
                return HumanizeDistance(theDate.Value);
            return "";
        }

        public static string ToRangeFrom(this DateTime from, DateTime to, 
            string dateFormat = "d MMM", string sep = "-",
            string css = "", string empty = "")
        {
            var text = empty;
            if (from == DateTime.MinValue || from == DateTime.MaxValue)
                return text;

            if (!css.IsNullOrWhitespace())
                sep = String.Format("<span class='{0}'>&nbsp;{1}&nbsp;</span>", css, sep);

            return String.Format("{0} {1} {2}",
                    from.ToString(dateFormat),
                    sep,
                    to.ToString(dateFormat));
        }

        private static Int32 ComputeYears(DateTime dob)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dob.Year * 100 + dob.Month) * 100 + dob.Day;

            var years = (a - b) / 10000;
            return years;
        }




        // CultureInfo
        public static List<string> WeekDays(this CultureInfo theCulture)
        {
            var listOfDayNames = new List<string>(theCulture.DateTimeFormat.DayNames);
            var indexOfFirstDayOfWeek = (int)theCulture.DateTimeFormat.FirstDayOfWeek;
            for (var i = 0; i < indexOfFirstDayOfWeek; i++)
            {
                var day = listOfDayNames[i];
                listOfDayNames.Remove(day);
                listOfDayNames.Insert(listOfDayNames.Count, day);
            }
            return listOfDayNames;
        }
    }
}