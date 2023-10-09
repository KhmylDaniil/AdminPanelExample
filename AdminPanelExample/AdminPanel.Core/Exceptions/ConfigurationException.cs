namespace AdminPanel.Core.Exceptions
{
    /// <summary>
    /// Exception for configuration errors
    /// </summary>
    public class ConfigurationException : ApplicationSystemException
    {
        /// <summary>
        /// ctor for <see cref="ConfigurationException"/>
        /// </summary>
        /// <param name="sectionName">configuration section name</param>
        public ConfigurationException(string sectionName) : base($"Configuration section {sectionName} is missing.") { }
    }
}
