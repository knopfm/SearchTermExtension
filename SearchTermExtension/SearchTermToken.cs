using System.Text.RegularExpressions;

namespace SearchTermExtension
{
    /// <summary>
    /// A search term token.
    /// </summary>
    public class SearchTermToken
    {
        /// <summary>
        /// The token value type.
        /// </summary>
        public SearchTermTokenType Type { get; }

        /// <summary>
        /// The token value flags.
        /// </summary>
        public SearchTermTokenFlags Flags { get; }

        /// <summary>
        /// The token value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a new token with given parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="value">The value.</param>
        public SearchTermToken(SearchTermTokenType type, SearchTermTokenFlags flags, string value)
        {
            Type = type;
            Value = value;
            Flags = flags;
        }
    }

    /// <summary>
    /// A search term token type.
    /// </summary>
    public enum SearchTermTokenType
    {
        None = Contains,
        Contains = 0,
        StartsWith = 1,
        EndsWith = 2,
    }

    /// <summary>
    /// A search term token flag.
    /// </summary>
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
