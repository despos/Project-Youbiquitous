///////////////////////////////////////////////////////////////////
//
// Youbiquitous v1.0 .NET Core
// Author: Dino Esposito
//

using System;
using System.Linq;
using System.Reflection;

namespace Expoware.Youbiquitous.Core.Extensions
{
    /// <summary>
    /// A static class for reflection functions
    /// </summary>
    public static class CopyExtensions
    {
        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="skipTheseProps">Properties to skip</param>
        public static void CopyPropertiesTo(this object source, object destination, params string[] skipTheseProps)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Invalid source/destination");

            // Getting the Types of the objects
            var typeDest = destination.GetType();
            var typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            var srcProps = typeSrc.GetProperties();
            foreach (var srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                    continue;
                var targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                    continue;
                if (!targetProperty.CanWrite)
                    continue;
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                    continue;
                if ((targetProperty.GetSetMethod() == null))
                    continue;
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                    continue;
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                    continue;
                if (skipTheseProps.Contains(srcProp.Name, StringComparer.CurrentCultureIgnoreCase))
                    continue;

                // Passed all tests, lets set the value
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }
        }
    }
}