using System.Text.RegularExpressions;

namespace SearchTermExtension
{
    public class SearchTermExpression
    {
        private readonly SearchTermToken[] searchTokens;

        public IEnumerable<SearchTermToken> SearchTokens => searchTokens;

        public SearchTermExpression(SearchTermToken[] searchTokens)
        {
            this.searchTokens = searchTokens;
        }

        public bool Evaluate<TSource>(TSource source, params Func<TSource, string>[] properties)
        {
            return searchTokens.All(token =>
            {
                bool isInResult = false;
                foreach (var property in properties)
                {
                    isInResult = isInResult || EvaluateTerm(token, property.Invoke(source));
                }
                isInResult ^= token.Flags.HasFlag(SearchTermTokenFlags.Not);
                return isInResult;
            });
        }

        public static bool EvaluateTerm(SearchTermToken token, string propertyString)
        {
            if (token.Flags.HasFlag(SearchTermTokenFlags.Exact))
                return string.Equals(propertyString, token.Value, token.Flags.HasFlag(SearchTermTokenFlags.CaseInvariant) ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture);
            if (token.Flags.HasFlag(SearchTermTokenFlags.Regex))
                return propertyString
                        .Split(SearchTerm.SearchStringSplitter, StringSplitOptions.RemoveEmptyEntries)
                        .Any(x => new Regex(token.Value, token.Flags.HasFlag(SearchTermTokenFlags.CaseInvariant) ? RegexOptions.IgnoreCase : RegexOptions.None).IsMatch(x))
                        ;
            return propertyString
                    .Split(SearchTerm.SearchStringSplitter, StringSplitOptions.RemoveEmptyEntries)
                    .Any(x => x.Contains(token.Value, token.Flags.HasFlag(SearchTermTokenFlags.CaseInvariant) ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture))
                    ;
        }
    }
}
