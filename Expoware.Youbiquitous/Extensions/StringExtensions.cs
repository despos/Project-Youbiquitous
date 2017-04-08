///////////////////////////////////////////////////////////////////
//
// Youbiquitous
// Author: Dino Esposito
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Expoware.Youbiquitous.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Boolean ContainsAny(this String theString, params String[] args)
        {
            var temp = theString.ToLower();
            return args.Any(s => temp.Contains(s.ToLower()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="newString"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static String ReplaceAny(this String theString, String newString, params String[] tokens)
        {
            return tokens.Aggregate(theString, (current, t) => current.Replace(t, newString));
        }

        /// <summary>
        /// Indicate whether a given string equals any of the specified substrings. 
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="args">List of possible matches</param>
        /// <returns>True/False</returns>
        public static Boolean EqualsAny(this String theString, params String[] args)
        {
            return args.Any(token => theString.Equals(token, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Indicate whether the given string is NULL or empty or whitespace.
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <returns>True/False</returns>
        public static Boolean IsNullOrWhitespace(this String theString)
        {
            return String.IsNullOrWhiteSpace(theString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theStringArray"></param>
        /// <returns></returns>
        public static String[] RemoveEmpty(this IEnumerable<String> theStringArray)
        {
            return theStringArray.Where(s => !s.IsNullOrWhitespace()).ToArray();
        }

        /// <summary>
        /// Turn the string into a boolean (accepting yes/no, y/n, 1/0)
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static bool ToBool(this String theString)
        {
            if (theString.IsNullOrWhitespace())
                return false;
            theString = theString.ToLower();
            return theString.ToLower().EqualsAny("yes", "y", "1", "+", "true");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="defaultNumber"></param>
        /// <returns></returns>
        public static Int32 ToInt(this String theString, Int32 defaultNumber = 0)
        {
            if (theString.IsNullOrWhitespace())
                return defaultNumber;
            Int32 number;
            var success = Int32.TryParse(theString, out number);
            if (!success)
            {
                if (theString.Contains("."))
                    theString = theString.SubstringTo(".");
                decimal number2;
                success = decimal.TryParse(theString, out number2);
                if (success)
                    number = (int)number2;
            }
            return success ? number : defaultNumber;
        }

        /// <summary>
        /// Parse a given string to a date.
        /// </summary>
        /// <param name="theString">String to parse</param>
        /// <param name="defaultDate">Date to return in case of failure</param>
        /// <returns>Date</returns>
        public static DateTime ToDate(this String theString, DateTime defaultDate = default(DateTime))
        {
            DateTime date;
            var success = DateTime.TryParse(theString, out date);
            return success ? date : defaultDate;  //DateTime.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static DateTime? TryAsCsvDate(this String theString)
        {
            if (theString.IsNullOrWhitespace())
                return null;
            var tokens = theString.Split(',');
            if (tokens.Length != 3)
                return null;
            try
            {
                return new DateTime(tokens[0].ToInt(), tokens[1].ToInt(), tokens[2].ToInt());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string StripHtml(this String theString)
        {
            var stripped = Regex.Replace(theString, @"<[^>]+>|&nbsp;", "").Trim();
            stripped = Regex.Replace(stripped, @"\s{2,}", " ");
            return stripped;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string InsertInto(this String theString, String format)
        {
            return String.Format(format, theString);
        }

        /// <summary>
        /// Get a slice of the provided string that begins at specified substring.
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="marker">Substring to locate</param>
        /// <param name="shouldIncludeMarker">Whether substring should be skipped or included</param>
        /// <returns>Substring</returns>
        public static String SubstringFrom(this String theString, String marker, Boolean shouldIncludeMarker = false)
        {
            var index = theString.IndexOf(marker, StringComparison.InvariantCultureIgnoreCase);
            if (index < 0)
                return theString;

            var startIndex = shouldIncludeMarker ? index : index + marker.Length;
            return theString.Substring(startIndex);
        }

        /// <summary>
        /// Get a slice of the provided string that ends at the specified substring.
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="marker">Substring to locate</param>
        /// <param name="shouldIncludeMarker">Whether substring should be skipped or included</param>
        /// <returns>Substring</returns>
        public static String SubstringTo(this String theString, String marker, Boolean shouldIncludeMarker = false)
        {
            var index = theString.IndexOf(marker, StringComparison.InvariantCultureIgnoreCase);
            if (index < 0)
                return theString;

            var endIndex = shouldIncludeMarker ? index + marker.Length : index;
            return theString.Substring(0, endIndex);
        }

        /// <summary>
        /// Get a slice of the provided string included between markers (not included)
        /// </summary>
        /// <param name="theString">String to process</param>
        /// <param name="marker1">Initial substring</param>
        /// <param name="marker2">Ending substring</param>
        /// <returns>Substring</returns>
        public static String SubstringBetween(this String theString, String marker1, String marker2)
        {
            var temp = theString.SubstringFrom(marker1);
            return temp.SubstringTo(marker2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="emptyText"></param>
        /// <returns></returns>
        public static String IfEmptyThen(this String theString, String emptyText = "")
        {
            if (theString.IsNullOrWhitespace())
                return emptyText;
            return theString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="okayIfEmpty"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this String theString, bool okayIfEmpty = false)
        {
            if (theString.IsNullOrWhitespace())
                return okayIfEmpty;

            try
            {
                return Regex.IsMatch(theString,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime? ToTime(this string time)
        {
            if (time.IsNullOrWhitespace())
                return null;

            var tokens = time.Split(':');
            if (tokens.Length != 2)
                return null;
            var date = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.Today.Day,
                tokens[0].ToInt(),
                tokens[1].ToInt(),
                0);
            return date;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static IList<int> ToIntList(this string theString, char separator = ',')
        {
            const int naN = -9999999;

            var list = new List<int>();
            if (theString.IsNullOrWhitespace())
                return list;

            var tokens = theString.Split(separator);
            list.AddRange(tokens.Select(t => t.ToInt(naN)).Where(number => number != naN));
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="defaultText"></param>
        /// <param name="css"></param>
        /// <param name="mailto"></param>
        /// <returns></returns>
        public static string ToDefault(this string theString, string defaultText = "", string css = "", string mailto = "<a href=mailto:{0}>{0}</a>")
        {
            if (theString.IsNullOrWhitespace())
            {
                return css.IsNullOrWhitespace()
                    ? defaultText
                    : String.Format("<span class='{0}'>{1}</span>", css, defaultText);
            }
            return theString.Mailto(mailto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string Capitalize(this string theString)
        {
            if (theString.IsNullOrWhitespace())
                return theString;

            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(theString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string EnsureTrimmed(this string theString)
        {
            if (theString.IsNullOrWhitespace())
                return null;
            return theString.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="textFormat"></param>
        /// <returns></returns>
        public static string Mailto(this string theString, string textFormat = "{0}")
        {
            if (!theString.IsValidEmail())
                return theString;
            var anchorText = String.Format(textFormat, theString);
            return String.Format("<a href=mailto:{0}>{1}</a>", theString, anchorText);
        }
    }
}