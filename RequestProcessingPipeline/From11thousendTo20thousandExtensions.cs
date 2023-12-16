namespace RequestProcessingPipeline
{
    public static class From11thousendTo20thousandExtensions
    {
        public static IApplicationBuilder UseFrom11thousendTo20thousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From11thousendTo20thousandMidleware>();
        }
    }
}
