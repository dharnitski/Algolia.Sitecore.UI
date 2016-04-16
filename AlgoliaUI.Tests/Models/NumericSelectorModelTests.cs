using AlgoliaUI.Code.Models;
using FluentAssertions;
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.FakeDb;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Tests.Models
{
    [TestFixture]
    public class NumericSelectorModelTests
    {
        [TestCase(">=", ">=")]
        [TestCase("=", "=")]
        public void OperatorTest(string key, string expected)
        {
            var valueId = ID.NewID;
            using (var db = new Db
            {
                new DbItem("value", valueId)
                {
                    {"key", key }
                },
                new DbItem("home")
                {
                    {"Operator", valueId.ToString()}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new NumericSelectorModel { Rendering = rendering };

                var actual = sut.Operator.ToString();
                actual.Should().Be(expected);
            }
        }

        [Test]
        public void OptionsTest()
        {
            using (var db = new Db
            {
                new DbItem("Home")
                {
                    {
                        "Options",
                        @"8=8%20per%20page&16=16%20per%20page&32=32%20per%20page"
                    }
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new NumericSelectorModel { Rendering = rendering };

                var expected = sut.Options;
                expected.ToHtmlString()
                    .Should()
                    .Be(
                        "{ value: 8, label: '8 per page' },\r\n{ value: 16, label: '16 per page' },\r\n{ value: 32, label: '32 per page' }");
            }
        }
    }
}
