using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;
using AdminPanel.Core.Exceptions;
using AdminPanel.Core.Entities;
using AdminPanel.Core;

namespace AdminPanel.WebApi
{
    /// <summary>
    /// Exception filter
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor for <see cref="ExceptionFilter"/>
        /// </summary>
        /// <param name="logger">logger</param>
        public ExceptionFilter(ILogger logger) => _logger = logger;

        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case EntityNotFoundException<User> or EntityNotFoundException<Role>:
                    _logger.Error(context.Exception.Message, context.Exception);
                    context.Result = new ObjectResult(context.Exception.Message) { StatusCode = StatusCodes.Status404NotFound };
                    break;
                case RequestValidationException ex when ex.Message == Constants.IncorrectLoginOrPassword:
                    context.Result = new ObjectResult(context.Exception.Message) { StatusCode = StatusCodes.Status401Unauthorized };
                    break;
                case RequestValidationException:
                    context.Result = new ObjectResult(context.Exception.Message) { StatusCode = StatusCodes.Status422UnprocessableEntity};
                    break;
                default:
                    _logger.Error(context.Exception.Message, context.Exception);
                    context.Result = new ObjectResult(context.Exception.Message) { StatusCode = StatusCodes.Status500InternalServerError };
                    break;
            }
        }
    }
}
