namespace RequestProcessingPipeline
{
    public static class From100To1000Extensions
    {
        public static IApplicationBuilder UseFrom100To1000(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From100To1000Midleware>();
        }
    }
}
