///////////////////////////////////////////////////////////////////
//
// Youbiquitous v1.0 .NET Core
// Author: Dino Esposito
//

namespace Expoware.Youbiquitous.Core.Extensions
{
    public static class BoolExtensions
    {
        /// <summary>
        /// Renders yes/no strings for the boolean
        /// </summary>
        /// <param name="theOption">Boolean value</param>
        /// <param name="yes">YES string</param>
        /// <param name="no">NO string</param>
        /// <returns>String</returns>
        public static string Humanize(this bool theOption, string yes = "Yes", string no = "No")
        {
            return theOption
                ? yes
                : no;
        }

        /// <summary>
        /// Renders HTML yes/no strings for the boolean
        /// </summary>
        /// <param name="theOption"></param>
        /// <param name="yes"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string HumanizeHtml(this bool theOption, string yes = "Yes", string no = "No")
        {
            return theOption
                ? $"<span class='text-success'>{yes}</span>"
                : $"<span class='text-danger'>{no}</span>";
        }

        /// <summary>
        /// Turns the boolean value into a JavaScript boolean value
        /// </summary>
        /// <param name="theOption">Boolean value</param>
        /// <returns>JavaScript boolean string</returns>
        public static string ToJavaScriptBoolean(this bool theOption)
        {
            return theOption.ToString().ToLower();
        }

        /// <summary>
        /// 1 if true, 0 if false
        /// </summary>
        /// <param name="theOption">Boolean value</param>
        /// <returns>Numeric value</returns>
        public static int ToInt(this bool theOption)
        {
            return theOption ? 1 : 0;
        }

        /// <summary>
        /// Returns a FA icon for the boolean value
        /// </summary>
        /// <param name="theValue"></param>
        /// <param name="faTrue">FA icon for true</param>
        /// <param name="faFalse">FA icon for false (or empty)</param>
        /// <returns></returns>
        public static string ToFaIcon(this bool theValue, string faTrue = "fa-check", string faFalse = "")
        {
            if (!theValue)
                return !faFalse.IsNullOrWhitespace()
                    ? $"<i class='fa {faFalse}'></i>"
                    : faFalse;
            return $"<i class='fa {faTrue}'></i>";
        }
    }
}