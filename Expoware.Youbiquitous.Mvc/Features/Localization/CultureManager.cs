///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Expoware.Youbiquitous.Mvc.Features.Localization
{
    public class CultureManager
    {
        protected CultureManager()
        {
            LanguageCodes = new List<string>();
        }

        public List<string> LanguageCodes { get; private set; }

        /// <summary>
        /// Current culture
        /// </summary>
        /// <returns></returns>
        public CultureInfo Current()
        {
            return CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Return the list of alternate cultures (all but the current)
        /// </summary>
        /// <returns></returns>
        public IList<CultureInfo> AlternateCultures()
        {
            var list = LanguageCodes
                .Where(l => !l.Equals(Current().Name, StringComparison.InvariantCultureIgnoreCase))
                .Select(l => new CultureInfo(l))
                .ToList();
            return list;
        }

        /// <summary>
        /// Empty manager
        /// </summary>
        /// <returns></returns>
        public static CultureManager Empty()
        {
            return new CultureManager();
        }

        /// <summary>
        /// Load supported cultures in the framework
        /// </summary>
        /// <param name="cultures">Comma-separated list of culture strings (ie, it-IT)</param>
        /// <returns></returns>
        public static CultureManager Import(string cultures = "")
        {
            var manager = new CultureManager();
            if (string.IsNullOrWhiteSpace(cultures))
                return manager;
            var tokens = cultures.Trim(' ', ',', ';').Split(',');
            if (tokens.Length == 0)
                return manager;

            foreach(var t in tokens)
                manager.LanguageCodes.Add(t.Trim());
            return manager;
        }
    }
}