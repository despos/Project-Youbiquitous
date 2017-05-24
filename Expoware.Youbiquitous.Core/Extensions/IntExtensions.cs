///////////////////////////////////////////////////////////////////
//
// Youbiquitous v1.0 .NET Core
// Author: Dino Esposito
//

using System.Globalization;

namespace Expoware.Youbiquitous.Core.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Converts to a string and uses the provided empty value if less than given threshold
        /// </summary>
        /// <param name="theNumber">Integer to convert</param>
        /// <param name="min">Threshold</param>
        /// <param name="empty">Empty string</param>
        /// <param name="format">Format for string conversion</param>
        /// <returns></returns>
        public static string ToStringOrEmpty(this int theNumber, int min = 0, string empty = "", string format = "{0}")
        {
            return theNumber <= min 
                ? empty 
                : string.Format(format, theNumber);
        }

        /// <summary>
        /// Converts to a string and uses the provided empty value if less than given threshold
        /// </summary>
        /// <param name="theNumber">Double to convert</param>
        /// <param name="min">Threshold</param>
        /// <param name="empty">Empty string</param>
        /// <param name="format">Format for string conversion</param>
        /// <returns></returns>
        public static string ToStringOrEmpty(this double theNumber, int min = 0, string empty = "", string format = "{0}")
        {
            return theNumber <= min 
                ? empty 
                : string.Format(format, theNumber);
        }

        /// <summary>
        /// Converts to a string and uses the provided empty value if greater than given threshold
        /// </summary>
        /// <param name="theNumber">Integer to convert</param>
        /// <param name="max">Threshold</param>
        /// <param name="empty">Empty string</param>
        /// <param name="format">Format for string conversion</param>
        /// <returns></returns>
        public static string ToStringIToStringOrEmptyOver(this int theNumber, int max = 0, string empty = "", string format = "{0}")
        {
            return theNumber >= max 
                ? empty 
                : string.Format(format, theNumber);
        }

        /// <summary>
        /// Converts to a string and uses the provided empty value if greater than given threshold
        /// </summary>
        /// <param name="theNumber">Double to convert</param>
        /// <param name="max">Threshold</param>
        /// <param name="empty">Empty string</param>
        /// <param name="format">Format for string conversion</param>
        /// <returns></returns>
        public static string ToStringIfGreaterThan(this double theNumber, int max = 0, string empty = "", string format = "{0}")
        {
            return theNumber >= max 
                ? empty 
                : string.Format(format, theNumber);
        }      
        
        /// <summary>
        /// Lower down the integer
        /// </summary>
        /// <param name="theNumber">Number</param>
        /// <param name="lower">Returns this if lower</param>
        /// <returns></returns>
        public static int ToLowerBound(this int theNumber, int lower)
        {
            return theNumber < lower ? lower : theNumber;
        }

        /// <summary>
        /// Rounds up the integer
        /// </summary>
        /// <param name="theNumber">Number</param>
        /// <param name="upper">Returns this if greater</param>
        public static int ToUpperBound(this int theNumber, int upper)
        {
            return theNumber > upper ? upper : theNumber;
        }

        /// <summary>
        /// Turns Fahrenheit degrees into Celsius
        /// </summary>
        /// <param name="fahrenheit"></param>
        /// <returns></returns>
        public static int ToCelsius(this int fahrenheit)
        {
            return (int)((5.0 / 9.0) * (fahrenheit - 32));
        }

        /// <summary>
        /// Turns Fahrenheit degrees to Celsius or empty string
        /// </summary>
        /// <param name="fahrenheit"></param>
        /// <param name="zero"></param>
        /// <returns></returns>
        public static string ToCelsiusOrDefault(this int fahrenheit, string zero = "")
        {
            return fahrenheit == 0 
                ? zero 
                : ((int)((5.0 / 9.0) * (fahrenheit - 32))).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Checks boundaries
        /// </summary>
        /// <param name="theNumber"></param>
        /// <param name="min">Lower bound</param>
        /// <param name="max">Upper bound</param>
        /// <returns></returns>
        public static bool Between(this int theNumber, int min, int max)
        {
            return theNumber >= min && theNumber <= max;
        }

        /// <summary>
        /// Checks boundaries strictly
        /// </summary>
        /// <param name="theNumber"></param>
        /// <param name="min">Lower bound</param>
        /// <param name="max">Upper bound</param>
        /// <returns></returns>
        public static bool StrictBetween(this int theNumber, int min, int max)
        {
            return theNumber > min && theNumber < max;
        }

        /// <summary>
        /// Increments up to given threshold
        /// </summary>
        /// <param name="theNumber">Number</param>
        /// <param name="max">Max value in the supported range</param>
        /// <returns></returns>
        public static int Increment(this int theNumber, int max = int.MaxValue)
        {
            if (theNumber < max)
                theNumber++;
            return theNumber;
        }

        /// <summary>
        /// Decrements down to given threshold
        /// </summary>
        /// <param name="theNumber">Number</param>
        /// <param name="min">Min value in the supported range</param>
        /// <returns></returns>
        public static int Decrement(this int theNumber, int min = int.MinValue)
        {
            if (theNumber > min)
                theNumber--;
            return theNumber;
        }

        /// <summary>
        /// Formats given number with respect to plural rules (1 year or 2 years)
        /// </summary>
        /// <param name="theNumber">Number</param>
        /// <param name="singular">Term in the singular form</param>
        /// <param name="plural">Term in the plural form</param>
        /// <returns></returns>
        public static string PluralizedFormat(this int theNumber, string singular = "", string plural = "")
        {
            return $"{theNumber} {(theNumber == 1 ? singular : plural)}";
        }

        /// <summary>
        /// True if it's even
        /// </summary>
        /// <param name="theNumber">Number</param>
        /// <returns></returns>
        public static bool IsEven(this int theNumber)
        {
            return theNumber % 2 == 0;
        }

        /// <summary>
        /// True if it's odd
        /// </summary>
        /// <param name="theNumber">Number</param>
        /// <returns></returns>
        public static bool IsOdd(this int theNumber)
        {
            return theNumber % 2 == 1;
        }
    }
}