using AlgoliaUI.Code.Models;
using FluentAssertions;
using NUnit.Framework;
using Sitecore.FakeDb;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Tests.Models
{
    [TestFixture]
    public class RefinementListModelTests
    {
        [TestCase("", "")]
        [TestCase(@"'&lt;div class=""facet - title""&gt;Materials&lt;/div class=""facet - title""&gt;'",
            "header: '<div class=\"facet - title\">Materials</div class=\"facet - title\">',")]
        public void HeaderTemplateTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Header Template", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new RefinementListModel {Rendering = rendering};

                var actual = sut.HeaderTemplate.ToString();
                actual.Should().Be(expected);
            }
        }

        [TestCase("", "")]
        [TestCase(@"'&lt;div class=""facet - title""&gt;Materials&lt;/div class=""facet - title""&gt;'",
            "item: '<div class=\"facet - title\">Materials</div class=\"facet - title\">',")]
        public void ItemTemplateTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Item Template", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new RefinementListModel { Rendering = rendering };

                var actual = sut.ItemTemplate.ToString();
                actual.Should().Be(expected);
            }
        }
    }
}
