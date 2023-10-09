namespace AdminPanel.Core.Exceptions
{
    /// <summary>
    /// Request is not valid exception
    /// </summary>
    public class RequestValidationException: Exception
    {
        /// <summary>
        /// ctor for <see cref="RequestValidationException"/>
        /// </summary>
        /// <param name="message">Message</param>
        public RequestValidationException(string message) : base(message) { }
    }
}
