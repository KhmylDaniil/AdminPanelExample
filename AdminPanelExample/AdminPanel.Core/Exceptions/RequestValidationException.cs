namespace AdminPanel.Core.Exceptions
{
    /// <summary>
    /// Request is not valid exception
    /// </summary>
    public class RequestValidationException: Exception
    {
        public RequestValidationException(string message) : base(message) { }
    }
}
