namespace SearchTermExtension
{
    /// <summary>
    /// Parsing class for the <see cref="string"/> search term to a <see cref="SearchTermExpression"/>.
    /// </summary>
    public class SearchTerm
    {
        internal static readonly char[] SearchStringSplitter = { ' ', ';', ',', '/' };

        /// <summary>
        /// The default flags for parsing a search term.
        /// </summary>
        public static SearchTermTokenFlags DefaultFlags { get; set; } = SearchTermTokenFlags.CaseInvariant;

        /// <summary>
        /// Parses a search term to its words an determs the flags for searching.
        /// <code>var term = SearchTerm.Parse("input");
        /// var results = source.SearchWithTerm(x => x.Property1).ApplyTerm(term);</code>
        /// </summary>
        /// <param name="term">The input search term.</param>
        /// <returns>A instance of <see cref="SearchTermExpression"/>, which representates the search term for searching.</returns>
        public static SearchTermExpression Parse(string term)
        {
            int startIndex = 0;
            SearchTermTokenFlags tokenFlags = DefaultFlags;
            List<SearchTermToken> resultTokens = new();
            for (int i = 0; i < term.Length; i++)
            {
                char c = term[i];
                if (SearchStringSplitter.Contains(c))
                {
                    if (tokenFlags.HasFlag(SearchTermTokenFlags.Exact))
                        continue;
                    if (startIndex == i)
                    {
                        startIndex = i + 1;
                        continue;
                    }
                    if (tokenFlags.HasFlag(SearchTermTokenFlags.Regex))
                        resultTokens.Add(new(SearchTermTokenType.Contains, tokenFlags, RegexPreprocess(term[startIndex..i], tokenFlags.HasFlag(SearchTermTokenFlags.Exact))));
                    else
                        resultTokens.Add(new(SearchTermTokenType.Contains, tokenFlags, term[startIndex..i]));
                    startIndex = i + 1;
                    tokenFlags = DefaultFlags;
                }

                if ((c == '!' || c == '-') && !tokenFlags.HasFlag(SearchTermTokenFlags.Not) && !tokenFlags.HasFlag(SearchTermTokenFlags.Exact))
                {
                    tokenFlags |= SearchTermTokenFlags.Not;
                    startIndex = i + 1;
                    continue;
                }

                if (c == '?' || c == '*')
                {
                    tokenFlags |= SearchTermTokenFlags.Regex;
                }

                if (c == '"')
                {
                    if (!tokenFlags.HasFlag(SearchTermTokenFlags.Exact))
                    {
                        tokenFlags |= SearchTermTokenFlags.Exact;
                        startIndex = i + 1;
                        continue;
                    }
                    else
                    {
                        if (tokenFlags.HasFlag(SearchTermTokenFlags.Regex))
                            resultTokens.Add(new(SearchTermTokenType.Contains, tokenFlags, RegexPreprocess(term[startIndex..i], tokenFlags.HasFlag(SearchTermTokenFlags.Exact))));
                        else
                            resultTokens.Add(new(SearchTermTokenType.Contains, tokenFlags, term[startIndex..i]));
                        startIndex = i + 1;
                        tokenFlags = DefaultFlags;
                    }
                }
            }
            if (startIndex < term.Length - 1)
                if (tokenFlags.HasFlag(SearchTermTokenFlags.Regex))
                    resultTokens.Add(new(SearchTermTokenType.Contains, tokenFlags, RegexPreprocess(term[startIndex..], tokenFlags.HasFlag(SearchTermTokenFlags.Exact))));
                else
                    resultTokens.Add(new(SearchTermTokenType.Contains, tokenFlags, term[startIndex..]));
            return new SearchTermExpression(resultTokens.ToArray());
        }

        /// <summary>
        /// Prepare a term for a regex usage.
        /// </summary>
        /// <param name="value">The input term.</param>
        /// <param name="exact">True if the term should be exact.</param>
        /// <returns>The prepared regex term.</returns>
        private static string RegexPreprocess(string value, bool exact)
        {
            value = value
                .Replace("\\", "\\\\")  // replace \ with \\
                .Replace(".", @"\.")    // replace dot-regex with dot-value
                .Replace("?", ".{1}")   // replace single char
                .Replace("*", @"\.*")   // replace wildcard
                ;
            return exact ? $"^{value}$" : value;
        }
    }
}
