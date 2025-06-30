using System.Diagnostics;

namespace CrudMVC.Controllers
{
    public class RequestTimingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Call the next middleware in the pipeline
            await _next(context);

            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation("Request [{Method}] {Path} took {Elapsed} ms",
                context.Request.Method,
                context.Request.Path,
                elapsedTime);
        }
    }

    public static class RequestTimingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTimingMiddleware>();
        }
    }


}
