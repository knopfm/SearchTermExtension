using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchTermExtension.Test
{
    internal class SearchSourceTest
    {
        [Test]
        public void OneTermNormal()
        {
            var terms = SearchTerm.Parse("term1");
            var result = CreateList.Search(AllProperties).ApplyTerm(terms);
            Assert.IsNotNull(result);
            Compare(CreateList, result, 0);
            Compare(CreateList, result, 1);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void TwoTermNormal()
        {
            var terms = SearchTerm.Parse("term1 term2");
            var result = CreateList.Search(AllProperties).ApplyTerm(terms);
            Assert.IsNotNull(result);
            Compare(CreateList, result, 0);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void DoubleTermNormal()
        {
            var terms = SearchTerm.Parse("term3 term3");
            var result = CreateList.Search(AllProperties).ApplyTerm(terms);
            Assert.IsNotNull(result);
            Compare(CreateList, result, 0);
            Compare(CreateList, result, 3);
            Assert.AreEqual(2, result.Count());
        }

        private static TestObject[] CreateList
            => new[]
            {
                new TestObject() { Property1 = "term1", Property2 = "term2", Property3 = "term3", Property4 = "term4", Property5 = "term5" },
                new TestObject() { Property1 = "term1", Property2 = "term1", Property3 = "term1", Property4 = "term1", Property5 = "term1" },
                new TestObject() { Property1 = "term2", Property2 = "term2", Property3 = "term2", Property4 = "term2", Property5 = "term2" },
                new TestObject() { Property1 = "term3", Property2 = "term3", Property3 = "term3", Property4 = "term3", Property5 = "term3" },
                new TestObject() { Property1 = "term4", Property2 = "term4", Property3 = "term4", Property4 = "term4", Property5 = "term4" },
                new TestObject() { Property1 = "term5", Property2 = "term5", Property3 = "term5", Property4 = "term5", Property5 = "term5" },
            };

        private static void Compare(IEnumerable<TestObject> expectedSource, IEnumerable<TestObject> actualSource, int expectedIndex)
        {
            Assert.LessOrEqual(actualSource.Count(), expectedSource.Count());
            Assert.Less(expectedIndex, expectedSource.Count());
            Assert.IsTrue(actualSource.Any(actual => Compare(expectedSource.ElementAt(expectedIndex), actual)));
        }

        private static bool Compare(TestObject expected, TestObject actual)
            => expected.Property1 == actual.Property1 &&
            expected.Property2 == actual.Property2 &&
            expected.Property3 == actual.Property3 &&
            expected.Property4 == actual.Property4 &&
            expected.Property5 == actual.Property5;

        private static Func<TestObject, string>[] AllProperties
            => new Func<TestObject, string>[]
                {
                    x => x.Property1,
                    x => x.Property2,
                    x => x.Property3,
                    x => x.Property4,
                    x => x.Property5,
                };
    }
}
