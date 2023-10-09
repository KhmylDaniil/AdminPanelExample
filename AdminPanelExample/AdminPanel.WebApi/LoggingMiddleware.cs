namespace AdminPanel.WebApi
{
    /// <summary>
    /// Logging middleware
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        /// <summary>
        /// Ctor for <see cref="LoggingMiddleware"/>
        /// </summary>
        /// <param name="next">delegate</param>
        /// <param name="loggerFactory">logger factory</param>
        public LoggingMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
            _next = next;
        }

        /// <summary>
        /// Invoke request
        /// </summary>
        /// <param name="context">http context</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            _logger.LogInformation("Request has been received {method} {path}", request.Method, request.Path);
            await _next(context);
            _logger.LogInformation("Request has been processed {method} {path}", request.Method, request.Path);
        }
    }
}
