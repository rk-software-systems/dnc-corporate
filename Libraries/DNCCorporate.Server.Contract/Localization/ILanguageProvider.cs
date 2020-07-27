using System.Collections.Generic;

namespace DNCCorporate.Server.Contract
{
    /// <summary>
    /// This class contains portal Language settings.
    /// </summary>
    public interface ILanguageProvider
    {
        /// <summary>
        /// Get all languages available for this portal.
        /// </summary>
        /// <returns></returns>
        IList<string> GetAvailableLanguages();

        /// <summary>
        /// Check if particular language is available on Portal
        /// </summary>
        /// <param name="languageCode">two letter language ISO code</param>
        /// <returns>Language availability</returns>
        bool IsLanguageAvailable(string languageCode);

        /// <summary>
        /// Get default portal language two letter ISO code (this setting is a generic one for all users).
        /// </summary>
        /// <returns>Two letter ISO code of the default language</returns>
        string GetDefaultLanguage();
    }
}
