namespace AdminPanel.Core.Exceptions
{
    /// <summary>
    /// Exception for general system errors
    /// </summary>
    public class ApplicationSystemException: Exception
    {
        /// <summary>
        /// ctor for <see cref="ApplicationSystemException"/>
        /// </summary>
        /// <param name="message">message</param>
        public ApplicationSystemException(string message): base(message) { }
    }
}
