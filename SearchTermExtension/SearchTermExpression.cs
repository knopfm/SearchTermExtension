using System.Text.RegularExpressions;

namespace SearchTermExtension
{
    /// <summary>
    /// The parsed search term.
    /// </summary>
    public class SearchTermExpression
    {
        private readonly SearchTermToken[] searchTokens;

        /// <summary>
        /// The search tokens.
        /// </summary>
        public IEnumerable<SearchTermToken> SearchTokens => searchTokens;

        /// <summary>
        /// Creates a <see cref="SearchTermExpression"/> with given tokens to search.
        /// </summary>
        /// <param name="searchTokens">The tokens.</param>
        public SearchTermExpression(SearchTermToken[] searchTokens)
        {
            this.searchTokens = searchTokens;
        }

        /// <summary>
        /// Evaluates a source if the expression matches.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <param name="source">The source to evaulate.</param>
        /// <param name="properties">The property selectors.</param>
        /// <returns>True if the source matches all tokens.</returns>
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

        /// <summary>
        /// Evaluates a single token if a <see cref="string"/> matches.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="propertyString">The source <see cref="string"/>.</param>
        /// <returns>True if the <see cref="string"/> matches the token.</returns>
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
