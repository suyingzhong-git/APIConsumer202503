namespace APIConsumer.BL.services
{
    public class ErrorHandlingMiddleware:IMiddleware
    {
        private readonly ILogger _logger;
        public ErrorHandlingMiddleware(ILogger logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context,RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                //deal with all unhandled errors
                var msg=ex.Message.ToString();
                _logger.LogInformation(msg);
            }
        }
    }
}
