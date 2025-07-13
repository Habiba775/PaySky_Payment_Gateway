using System.Diagnostics;

namespace API.MiddleWare
{
    public class ProfilingMiddleWares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleWares> _logger;

        public ProfilingMiddleWares(RequestDelegate next, ILogger<ProfilingMiddleWares> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await _next.Invoke(context);//ystna lehad response yrg3
            stopWatch.Stop();
            _logger.LogInformation($"Request `{context.Request.Path}` took `{stopWatch.ElapsedMilliseconds}` ms");


        }
    }
}
