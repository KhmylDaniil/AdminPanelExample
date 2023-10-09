
namespace AdminPanel.Core.Exceptions
{
    /// <summary>
    /// Exception for general system errors
    /// </summary>
    public class ApplicationSystemException: Exception
    {
        public ApplicationSystemException(string message): base(message) { }
    }
}
