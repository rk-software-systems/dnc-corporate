namespace DNCCorporate.Server.Contract
{
    /// <summary>
    /// This class is used to return general user / request related information
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Request working language
        /// </summary>
        string WorkingLanguage { get; }

        /// <summary>
        /// Default portal language
        /// </summary>
        string DefaultLanguage { get; }

        /// <summary>
        /// Name of working theme
        /// </summary>
        string CurrentTheme { get; }
    }
}
