namespace RequestProcessingPipeline
{
    public static class From20thousandTo100thousandExtensions
    {

        public static IApplicationBuilder UseFrom20thousandTo100thousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From20thousandTo100thousandMidleware>();
        }
    }
}
