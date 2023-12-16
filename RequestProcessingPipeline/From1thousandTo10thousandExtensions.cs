namespace RequestProcessingPipeline
{
    public static class From1thousandTo10thousandExtensions
    {
        public static IApplicationBuilder UseFrom1thousandTo10thousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From1thousandTo10thousandMidleware>();
        }
    }
}
