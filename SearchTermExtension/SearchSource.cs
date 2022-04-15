namespace SearchTermExtension
{
    public class SearchSource<TSource>
    {
        private readonly IEnumerable<TSource> source;
        private readonly Func<TSource, string>[] properties;

        public SearchSource(IEnumerable<TSource> source, params Func<TSource, string>[] properties)
        {
            this.source = source;
            this.properties = properties;
        }

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
