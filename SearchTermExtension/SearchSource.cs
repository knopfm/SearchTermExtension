namespace SearchTermExtension
{
    /// <summary>
    /// A search source with property selectors.
    /// </summary>
    /// <typeparam name="TSource">The source enumerable type.</typeparam>
    public class SearchSource<TSource>
    {
        private readonly IEnumerable<TSource> source;
        private readonly Func<TSource, string>[] properties;

        /// <summary>
        /// Creates a new instance of <see cref="SearchSource{TSource}"/> with a given source enumerable and there property selectors.
        /// </summary>
        /// <param name="source">The source enumerable.</param>
        /// <param name="properties">The property selectors.
        /// <code>x => x.Property1</code></param>
        public SearchSource(IEnumerable<TSource> source, params Func<TSource, string>[] properties)
        {
            this.source = source;
            this.properties = properties;
        }

        /// <summary>
        /// Applies a <see cref="SearchTermExpression"/> to the source enumerable and returns the matching results.
        /// </summary>
        /// <param name="termExpression">The search term expression.</param>
        /// <returns>Returns all matching results.</returns>
        public IEnumerable<TSource> ApplyTerm(SearchTermExpression termExpression)
        {
            foreach(var element in source.AsEnumerable())
            {
                if(termExpression.Evaluate(element, properties))
                    yield return element;
            }
        }
    }
}
