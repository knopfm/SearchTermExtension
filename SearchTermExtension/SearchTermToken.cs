using System.Text.RegularExpressions;

namespace SearchTermExtension
{
    public class SearchTermToken
    {
        public SearchTermTokenType Type { get; }
        public SearchTermTokenFlags Flags { get; }
        public string Value { get; }

        public SearchTermToken(SearchTermTokenType type, SearchTermTokenFlags flags, string value)
        {
            Type = type;
            Value = value;
            Flags = flags;
        }
    }

    public enum SearchTermTokenType
    {
        None = Contains,
        Contains = 0,
        StartsWith = 1,
        EndsWith = 2,
    }

    [Flags]
    public enum SearchTermTokenFlags
    {
        None = 0,
        CaseInvariant = 1,
        Not = 2,
        Exact = 4,
        Regex = 8,
    }
}
