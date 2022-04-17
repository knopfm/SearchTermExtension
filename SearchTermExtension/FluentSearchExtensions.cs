namespace SearchTermExtension
{
    public static class FluentSearchExtensions
    {
        /// <summary>
        /// Searches through a enumerable with given property selectors.
        /// <code>var term = SearchTerm.Parse("input");
        /// var result = source.SearchWithTerm(x => x.Property1).ApplyTerm(term);</code>
        /// </summary>
        /// <typeparam name="TSource">The enumerable type.</typeparam>
        /// <param name="sources">The source enumerable.</param>
        /// <param name="properties">The property selectors for the search.</param>
        /// <returns>Returns a <see cref="SearchSource{TSource}"/> for application of a search term.</returns>
        public static SearchSource<TSource> SearchWithTerm<TSource>(this IEnumerable<TSource> sources, params Func<TSource, string>[] properties)
        {
            return new SearchSource<TSource>(sources, properties);
        }
    }
}
