///////////////////////////////////////////////////////////////////
//
// Youbiquitous v1.0 .NET Core
// Author: Dino Esposito
//

using System.Globalization;

namespace Expoware.Youbiquitous.Core.Extensions
{
    public static class CultureInfoExtensions
    {
        public static string DisplayName(this CultureInfo culture)
        {
            return culture.Parent.NativeName.Capitalize();
        }

        public static string Code(this CultureInfo culture)
        {
            return culture.Name;
        }
    }
}