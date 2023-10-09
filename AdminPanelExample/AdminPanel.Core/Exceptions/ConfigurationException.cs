namespace AdminPanel.Core.Exceptions
{
    /// <summary>
    /// Exception for configuration errors
    /// </summary>
    public class ConfigurationException : ApplicationSystemException
    {
        public ConfigurationException(string sectionName) : base($"Configuration section {sectionName} is missing.") { }
    }
}
