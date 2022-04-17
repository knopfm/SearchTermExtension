namespace SearchTermExtension
{
    public class SearchTerm
    {
        internal static readonly char[] SearchStringSplitter = { ' ', ';', ',', '/' };

        public static SearchTermTokenFlags DefaultFlags { get; set; } = SearchTermTokenFlags.CaseInvariant;

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
