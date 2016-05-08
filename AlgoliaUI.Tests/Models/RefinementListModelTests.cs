using System.Web;
using AlgoliaUI.Code.Models;
using FluentAssertions;
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.FakeDb;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Tests.Models
{
    [TestFixture]
    public class RefinementListModelTests
    {
        [TestCase("", "")]
        [TestCase("'<div class=\"facet - title\">Materials</div class=\"facet - title\">'",
            "header: '<div class=\"facet - title\">Materials</div class=\"facet - title\">',")]
        [TestCase("template", "header: 'template',")]
        [TestCase("&", "header: '&',")]
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

                var actual = HttpUtility.HtmlEncode(sut.HeaderTemplate);
                actual.Should().Be(expected);
            }
        }

        [TestCase("", "")]
        [TestCase("'<div class=\"facet - title\">Materials</div class=\"facet - title\">'",
            "item: '<div class=\"facet - title\">Materials</div class=\"facet - title\">',")]
        [TestCase("template", "item: 'template',")]
        [TestCase("&", "item: '&',")]
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

                var actual = HttpUtility.HtmlEncode(sut.ItemTemplate);
                actual.Should().Be(expected);
            }
        }

        [TestCase("", "")]
        [TestCase("10", "limit: 10,")]
        public void LimitTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Limit", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new RefinementListModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.Limit.ToString());
                actual.Should().Be(expected);
            }
        }

        [TestCase("or", "or")]
        [TestCase("and", "and")]
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

                var sut = new RefinementListModel { Rendering = rendering };

                var actual = sut.Operator.ToString();
                actual.Should().Be(expected);
            }
        }
    }
}
