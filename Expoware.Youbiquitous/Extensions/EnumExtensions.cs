///////////////////////////////////////////////////////////////////
//
// Youbiquitous v1.0
// Author: Dino Esposito
//

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Expoware.Youbiquitous.Extensions
{
    public class EnumItem<T>
    {
        public string Description { get; set; } 
        public T Value { get; set; }
    }

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

        /// <summary>
        /// Returns the value of the [Description] attribute if any
        /// </summary>
        /// <param name="val">Enum value</param>
        /// <returns></returns>
        public static string GetDescription(this Enum val)
        {
            var field = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : val.ToString();
        }

        /// <summary>
        /// Returns all items in the enum as an EnumItem list
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static IList<EnumItem<T>> GetItems<T>(this Enum enumeration)
        {
            var enumType = enumeration.GetType();
            var values = Enum.GetValues(enumType);
            var items = new List<EnumItem<T>>();
            foreach (var v in values)
            {
                var enumValue = Enum.Parse(enumType, v.ToString());
                items.Add(GetDescriptionInternal<T>(enumValue));
            }
            return items;
        }

        #region PRIVATE
        private static EnumItem<T> GetDescriptionInternal<T>(object val)
        {
            var field = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            var enumItem = new EnumItem<T>
            {
                Description = attributes.Length > 0 ? attributes[0].Description : val.ToString(),
                Value = (T)val
            };
            return enumItem;
        }
        #endregion
    }
}