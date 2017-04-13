///////////////////////////////////////////////////////////////////
//
// Youbiquitous v1.0
// Author: Dino Esposito
//

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Expoware.Youbiquitous.Extensions
{
    public static class DateExtensions
    {
        private const string DateYears = "years";

        /// <summary>
        /// Formats the date as usual and returns the specified empty string otherwise
        /// </summary>
        /// <param name="theDate">Original date</param>
        /// <param name="format">Date format</param>
        /// <param name="empty">String if it's empty</param>
        /// <returns></returns>
        public static string ToStringOrEmpty(this DateTime theDate, string format, string empty = "")
        {
            if (theDate == DateTime.MinValue || theDate == DateTime.MaxValue)
                return empty;

            return theDate.ToString(format);
        }

        /// <summary>
        /// Formats the (nullable) date as usual and returns the specified empty string otherwise
        /// </summary>
        /// <param name="theDate">Original date</param>
        /// <param name="format">Date format</param>
        /// <param name="empty">String if it's empty</param>
        /// <returns>String</returns>
        public static string ToStringOrEmpty(this DateTime? theDate, string format, string empty = "")
        {
            return theDate.GetValueOrDefault().ToStringOrEmpty(format);
        }

        /// <summary>
        /// Date expressed as years (age)
        /// </summary>
        /// <param name="theDate">Original date</param>
        /// <param name="empty">String if it's empty</param>
        /// <returns>String</returns>
        public static string ToYearsOrEmpty(this DateTime theDate, string empty = "")
        {
            if (theDate == DateTime.MinValue)
                return empty;

            return ComputeYears(theDate).ToString();
        }

        /// <summary>
        /// Nullable date expressed as years (age)
        /// </summary>
        /// <param name="theDate">Original date</param>
        /// <param name="empty">String if it's empty</param>
        /// <returns></returns>
        public static string ToYearsOrEmpty(this DateTime? theDate, string empty = "")
        {
            return theDate.GetValueOrDefault().ToYearsOrEmpty(empty);
        }

        /// <summary>
        /// Compute years and adds "years"
        /// </summary>
        /// <param name="theDate">Original date</param>
        /// <param name="empty">String if it's empty</param>
        /// <returns>String</returns>
        public static string ToAge(this DateTime theDate, string empty = "")
        {
            if (theDate == DateTime.MinValue)
                return empty;

            return $"{ComputeYears(theDate)} {DateYears}";
        }

        /// <summary>
        /// Compute years and adds "years"
        /// </summary>
        /// <param name="theDate">Original nullable date</param>
        /// <param name="empty">String if it's empty</param>
        /// <returns>String</returns> 
        public static string ToAge(this DateTime? theDate, string empty = "")
        {
            return theDate.GetValueOrDefault().ToAge(empty);
        }

        /// <summary>
        /// Renders a date range nicely for the UI
        /// </summary>
        /// <param name="from">Initial date</param>
        /// <param name="to">Final date</param>
        /// <param name="dateFormat">Date format</param>
        /// <param name="sep">Range separator</param>
        /// <param name="css">Optional CSS </param>
        /// <param name="empty">String if it's empty</param>
        /// <returns></returns>
        public static string ToRangeFrom(this DateTime from, DateTime to, 
            string dateFormat = "d MMM", string sep = "-",
            string css = "", string empty = "")
        {
            var text = empty;
            if (from == DateTime.MinValue || from == DateTime.MaxValue)
                return text;

            if (!css.IsNullOrWhitespace())
                sep = $"<span class='{css}'>&nbsp;{sep}&nbsp;</span>";

            return $"{from.ToString(dateFormat)} {sep} {to.ToString(dateFormat)}";
        }

        /// <summary>
        /// Returns week days
        /// </summary>
        /// <param name="theCulture">Culture info</param>
        /// <returns>List of strings</returns>
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

        #region PRIVATE
        /// <summary>
        /// Calculate years to date
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        private static int ComputeYears(DateTime dob)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dob.Year * 100 + dob.Month) * 100 + dob.Day;

            var years = (a - b) / 10000;
            return years;
        }

        #endregion
    }
}