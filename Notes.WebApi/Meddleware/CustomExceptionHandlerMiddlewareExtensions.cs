namespace Notes.WebApi.Meddleware
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHanler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHanlerMiddleware>();
        }
    }
}
