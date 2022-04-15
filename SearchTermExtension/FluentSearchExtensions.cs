namespace SearchTermExtension
{
    public static class FluentSearchExtensions
    {
        public static SearchSource<TSource> Search<TSource>(this IEnumerable<TSource> sources, params Func<TSource, string>[] properties)
        {
            return new SearchSource<TSource>(sources, properties);
        }
    }
}
