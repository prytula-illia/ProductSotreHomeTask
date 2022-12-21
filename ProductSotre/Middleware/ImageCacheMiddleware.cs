using Microsoft.Extensions.Caching.Memory;

namespace ProductSotre.Middleware
{
    public class ImageCacheMiddleware
    {
        private readonly IMemoryCache _memoryCache;
        private readonly RequestDelegate _next;
        private const int CacheExpirationTime = 60;
        private const long CacheSize = 5;

        public ImageCacheMiddleware(IMemoryCache memoryCache, RequestDelegate next)
        {
            _memoryCache = memoryCache;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }

    public static class ImageCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageCaching(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageCacheMiddleware>();
        }
    }
}
