﻿using DNCCorporate.Server.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNCCorporate.Server.Services.Localization
{
    /// <summary>
    /// <see cref="ILanguageProvider"/> implementation
    /// </summary>
    public class LanguageProvider : ILanguageProvider
    {
        public IList<string> GetAvailableLanguages()
        {
            return new List<string>
            {
                "en",
                "ru"
            };
        }

        public string GetDefaultLanguage()
        {
            return "en";
        }

        public bool IsLanguageAvailable(string languageCode)
        {
            return GetAvailableLanguages()
                .Any(x => x.Equals(languageCode, StringComparison.OrdinalIgnoreCase));
        }
    }
}
