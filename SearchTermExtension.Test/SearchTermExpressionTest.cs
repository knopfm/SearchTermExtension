using NUnit.Framework;
using System;

namespace SearchTermExtension.Test
{
    internal class SearchTermExperssionTest
    {
        #region EvaluateTerm

        [Test]
        public void EvaluateExactNormal()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Exact, "term1"), "term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Exact, "term1"), "Term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Exact, "term1"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Exact, "term1"), "Term2"));
        }

        [Test]
        public void EvaluateExactCaseInvariant()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Exact | SearchTermTokenFlags.CaseInvariant, "term1"), "term1"));
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Exact | SearchTermTokenFlags.CaseInvariant, "term1"), "Term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Exact | SearchTermTokenFlags.CaseInvariant, "term1"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Exact | SearchTermTokenFlags.CaseInvariant, "term1"), "Term2"));
        }

        [Test]
        public void EvaluateNormal()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None, "term1"), "term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None, "term1"), "Term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None, "term1"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None, "term1"), "Term2"));
        }

        [Test]
        public void EvaluateCaseInvariant()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None | SearchTermTokenFlags.CaseInvariant, "term1"), "term1"));
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None | SearchTermTokenFlags.CaseInvariant, "term1"), "Term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None | SearchTermTokenFlags.CaseInvariant, "term1"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None | SearchTermTokenFlags.CaseInvariant, "term1"), "Term2"));
        }

        [Test]
        public void RegexNormal()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "term1"), "term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "term1"), "Term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "term1"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "term1"), "Term2"));
        }

        [Test]
        public void RegexCaseInvariant()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "term1"), "term1"));
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "term1"), "Term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "term1"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "term1"), "Term2"));
        }

        [Test]
        public void RegexOneCharacter()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "t.{1}rm1"), "term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "t.{1}rm1"), "Term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "t.{1}rm1"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "t.{1}rm1"), "Term2"));
        }

        [Test]
        public void RegexOneCharacterCaseInvariant()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "t.{1}rm1"), "term1"));
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "t.{1}rm1"), "Term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "t.{1}rm1"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "t.{1}rm1"), "Term2"));
        }

        [Test]
        public void RegexWildcard()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "ter\\.*"), "term1"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "ter\\.*"), "Term1"));
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "ter\\.*"), "term2"));
            Assert.IsFalse(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex, "ter\\.*"), "Term2"));
        }

        [Test]
        public void RegexWildcardCaseInvariant()
        {
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "ter\\.*"), "term1"));
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "ter\\.*"), "Term1"));
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "ter\\.*"), "term2"));
            Assert.IsTrue(SearchTermExpression.EvaluateTerm(
                new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Regex | SearchTermTokenFlags.CaseInvariant, "ter\\.*"), "Term2"));
        }
        #endregion

        #region Evaluate
        [Test]
        public void OnePropertyOneTokenNormal1()
        {
            var source = CreateTestObject1();
            var expression = new SearchTermExpression(new[] {
                    new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None , "term1"),
                });
            Assert.IsTrue(expression.Evaluate(source, x => x.Property1));
            Assert.IsFalse(expression.Evaluate(source, x => x.Property2));
            Assert.IsFalse(expression.Evaluate(source, x => x.Property3));
            Assert.IsFalse(expression.Evaluate(source, x => x.Property4));
            Assert.IsFalse(expression.Evaluate(source, x => x.Property5));
        }
        [Test]
        public void OnePropertyOneTokenNormal2()
        {
            var source = CreateTestObject2();
            var expression = new SearchTermExpression(new[] {
                    new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None , "term1"),
                });
            Assert.IsTrue(expression.Evaluate(source, x => x.Property1));
            Assert.IsTrue(expression.Evaluate(source, x => x.Property2));
            Assert.IsTrue(expression.Evaluate(source, x => x.Property3));
            Assert.IsTrue(expression.Evaluate(source, x => x.Property4));
            Assert.IsTrue(expression.Evaluate(source, x => x.Property5));
        }

        [Test]
        public void ThreePropertyOneTokenNormal()
        {
            var expression = new SearchTermExpression(new[] {
                    new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None , "term1"),
                });
            Assert.IsTrue(expression.Evaluate(CreateTestObject1(), x => x.Property1, x => x.Property2, x => x.Property3));
            Assert.IsFalse(expression.Evaluate(CreateTestObject1(), x => x.Property3, x => x.Property4, x => x.Property5));
            Assert.IsTrue(expression.Evaluate(CreateTestObject2(), x => x.Property1, x => x.Property2, x => x.Property3));
            Assert.IsTrue(expression.Evaluate(CreateTestObject2(), x => x.Property3, x => x.Property4, x => x.Property5));
        }

        [Test]
        public void FivePropertyOneTokenNormal()
        {
            var expression = new SearchTermExpression(new[] {
                    new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None , "term1"),
                });
            Assert.IsTrue(expression.Evaluate(CreateTestObject1(), AllProperties));
            Assert.IsTrue(expression.Evaluate(CreateTestObject2(), AllProperties));
        }

        [Test]
        public void FivePropertyTwoTokenNormal()
        {
            var expression = new SearchTermExpression(new[] {
                    new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None , "term1"),
                    new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.None , "term2"),
                });
            Assert.IsTrue(expression.Evaluate(CreateTestObject1(), AllProperties));
            Assert.IsFalse(expression.Evaluate(CreateTestObject2(), AllProperties));
        }

        [Test]
        public void FivePropertyOneNot1()
        {
            var expression = new SearchTermExpression(new[] {
                    new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Not , "term1"),
                });
            Assert.IsFalse(expression.Evaluate(CreateTestObject1(), AllProperties));
            Assert.IsFalse(expression.Evaluate(CreateTestObject2(), AllProperties));
        }

        [Test]
        public void FivePropertyOneNot2()
        {
            var expression = new SearchTermExpression(new[] {
                    new SearchTermToken(SearchTermTokenType.Contains, SearchTermTokenFlags.Not , "term2"),
                });
            Assert.IsFalse(expression.Evaluate(CreateTestObject1(), AllProperties));
            Assert.IsTrue(expression.Evaluate(CreateTestObject2(), AllProperties));
        }


        private static TestObject CreateTestObject1()
            => new()
            {
                Property1 = "term1",
                Property2 = "term2",
                Property3 = "term3",
                Property4 = "term4",
                Property5 = "term5",
            };

        private static TestObject CreateTestObject2()
            => new()
            {
                Property1 = "term1",
                Property2 = "term1",
                Property3 = "term1",
                Property4 = "term1",
                Property5 = "term1",
            };

        private static Func<TestObject, string>[] AllProperties
            => new Func<TestObject, string>[]
                {
                    x => x.Property1,
                    x => x.Property2,
                    x => x.Property3,
                    x => x.Property4,
                    x => x.Property5,
                };
        #endregion
    }
}
