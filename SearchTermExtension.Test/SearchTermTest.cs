using NUnit.Framework;
using System.Linq;

namespace SearchTermExtension.Test
{
    internal class SearchTermTest
    {
        [Test]
        public void EmptyParsing()
        {
            var result = SearchTerm.Parse("");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.SearchTokens.Count());
        }

        [Test]
        public void OneTermNormal()
        {
            {
                var result = SearchTerm.Parse("Term1");
                Assert.IsNotNull(result);
                TestResult(result, 0, "Term1", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
                Assert.AreEqual(1, result.SearchTokens.Count());
            }
        }

        [Test]
        public void TwoTermNormal()
        {
            {
                var result = SearchTerm.Parse("Term1 term2");
                Assert.IsNotNull(result);
                TestResult(result, 0, "Term1", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
                TestResult(result, 1, "term2", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
                Assert.AreEqual(2, result.SearchTokens.Count());
            }
        }

        [Test]
        public void ThreeTermNormal()
        {
            {
                var result = SearchTerm.Parse("term1 TERM2 3tERm");
                Assert.IsNotNull(result);
                TestResult(result, 0, "term1", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
                TestResult(result, 1, "TERM2", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
                TestResult(result, 2, "3tERm", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
                Assert.AreEqual(3, result.SearchTokens.Count());
            }
        }

        [Test]
        public void MultipleSplitters()
        {
            var result = SearchTerm.Parse(" , term1,term2  term3 ;/term4 ");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            TestResult(result, 1, "term2", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            TestResult(result, 2, "term3", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            TestResult(result, 3, "term4", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            Assert.AreEqual(4, result.SearchTokens.Count());

        }

        [Test]
        public void OneExactOneNormal()
        {
            var result = SearchTerm.Parse("\"term1 TERM2\" 3tERm");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1 TERM2", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            TestResult(result, 1, "3tERm", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            Assert.AreEqual(2, result.SearchTokens.Count());
        }

        [Test]
        public void OneNormalOneExactOneNormal()
        {
            var result = SearchTerm.Parse("term1 \"TERM2\" 3tERm");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            TestResult(result, 1, "TERM2", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            TestResult(result, 2, "3tERm", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            Assert.AreEqual(3, result.SearchTokens.Count());
        }

        [Test]
        public void TwoExactOneNormal()
        {
            var result = SearchTerm.Parse("\"term1\" \"TERM2\" 3tERm");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            TestResult(result, 1, "TERM2", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            TestResult(result, 2, "3tERm", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            Assert.AreEqual(3, result.SearchTokens.Count());
        }

        [Test]
        public void OneExactTwoNormal()
        {
            var result = SearchTerm.Parse("\"term1\" TERM2 3tERm");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            TestResult(result, 1, "TERM2", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            TestResult(result, 2, "3tERm", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            Assert.AreEqual(3, result.SearchTokens.Count());
        }

        [Test]
        public void OneExact()
        {
            var result = SearchTerm.Parse("\"term1\"");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            Assert.AreEqual(1, result.SearchTokens.Count());
        }

        [Test]
        public void ExactWithSplitters()
        {
            var result = SearchTerm.Parse("\"/ term1\\ ,;\"");
            Assert.IsNotNull(result);
            TestResult(result, 0, "/ term1\\ ,;", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            Assert.AreEqual(1, result.SearchTokens.Count());
        }

        [Test]
        public void NotNormal()
        {
            var result = SearchTerm.Parse("!term1 term2");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1", SearchTermTokenFlags.Not, SearchTermTokenType.Contains);
            TestResult(result, 1, "term2", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            Assert.AreEqual(2, result.SearchTokens.Count());
        }

        [Test]
        public void NormalNot()
        {
            var result = SearchTerm.Parse("term1 -term2");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1", SearchTermTokenFlags.None, SearchTermTokenType.Contains);
            TestResult(result, 1, "term2", SearchTermTokenFlags.Not, SearchTermTokenType.Contains);
            Assert.AreEqual(2, result.SearchTokens.Count());
        }

        [Test]
        public void NotExact()
        {
            var result = SearchTerm.Parse("!\"term1\"");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term1", SearchTermTokenFlags.Not | SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            Assert.AreEqual(1, result.SearchTokens.Count());
        }

        [Test]
        public void ExactNot()
        {
            var result = SearchTerm.Parse("\"!term1\"");
            Assert.IsNotNull(result);
            TestResult(result, 0, "!term1", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            Assert.AreEqual(1, result.SearchTokens.Count());
        }

        [Test]
        public void RegexOneChar()
        {
            var result = SearchTerm.Parse("t?rm1");
            Assert.IsNotNull(result);
            TestResult(result, 0, "t.{1}rm1", SearchTermTokenFlags.Regex, SearchTermTokenType.Contains);
            Assert.AreEqual(1, result.SearchTokens.Count());
        }

        [Test]
        public void RegexOneCharExact()
        {
            var result = SearchTerm.Parse("t?rm1 \"term2\"");
            Assert.IsNotNull(result);
            TestResult(result, 0, "t.{1}rm1", SearchTermTokenFlags.Regex, SearchTermTokenType.Contains);
            TestResult(result, 1, "term2", SearchTermTokenFlags.Exact, SearchTermTokenType.Contains);
            Assert.AreEqual(2, result.SearchTokens.Count());
        }

        [Test]
        public void RegexWildcard()
        {
            var result = SearchTerm.Parse("term*");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term\\.*", SearchTermTokenFlags.Regex, SearchTermTokenType.Contains);
            Assert.AreEqual(1, result.SearchTokens.Count());
        }
                
        [Test]
        public void Regex()
        {
            var result = SearchTerm.Parse("term?.");
            Assert.IsNotNull(result);
            TestResult(result, 0, "term.{1}\\.", SearchTermTokenFlags.Regex, SearchTermTokenType.Contains);
            Assert.AreEqual(1, result.SearchTokens.Count());
        }

        private static void TestResult(SearchTermExpression result, int index, string expectedValue, SearchTermTokenFlags expectedFlags, SearchTermTokenType expectedType)
        {
            Assert.Less(index, result.SearchTokens.Count());
            Assert.AreEqual(expectedValue, result.SearchTokens.ElementAt(index).Value);
            Assert.AreEqual(expectedFlags, result.SearchTokens.ElementAt(index).Flags);
            Assert.AreEqual(expectedType, result.SearchTokens.ElementAt(index).Type);
        }
    }
}
