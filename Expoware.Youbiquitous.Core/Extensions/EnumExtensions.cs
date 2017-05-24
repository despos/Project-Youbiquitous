///////////////////////////////////////////////////////////////////
//
// Youbiquitous v1.0 .NET Core
// Author: Dino Esposito
//

using System;

namespace Expoware.Youbiquitous.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Cast to given type (if possible)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="val">value</param>
        /// <returns></returns>
        public static T As<T>(this Enum val)
        {
            var enumType = val.GetType();
            var enumValue = Enum.Parse(enumType, val.ToString());
            return (T)enumValue;
        }

        /// <summary>
        /// Get an enum value from matching string name
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="name">String of enum entry</param>
        /// <returns></returns>
        public static T Parse<T>(this string name) where T : struct
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }
}