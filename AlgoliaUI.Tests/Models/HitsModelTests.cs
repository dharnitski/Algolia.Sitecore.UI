using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AlgoliaUI.Code.Models;
using FluentAssertions;
using NUnit.Framework;
using Sitecore.FakeDb;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Tests.Models
{
    public class HitsModelTests
    {
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

                var sut = new HitsModel {Rendering = rendering};

                var actual = HttpUtility.HtmlEncode(sut.ItemTemplate);
                actual.Should().Be(expected);
            }
        }

        [TestCase("", "")]
        [TestCase("'<div class=\"facet - title\">Materials</div class=\"facet - title\">'",
            "empty: '<div class=\"facet - title\">Materials</div class=\"facet - title\">',")]
        [TestCase("template", "empty: 'template',")]
        [TestCase("&", "empty: '&',")]
        public void EmptyTemplateTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Empty Template", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new HitsModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.EmptyTemplate);
                actual.Should().Be(expected);
            }
        }

        [TestCase("", "")]
        [TestCase("10", "hitsPerPage: 10,")]
        public void HitsPerPageTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Hits Per Page", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new HitsModel {Rendering = rendering};

                var actual = HttpUtility.HtmlEncode(sut.HitsPerPage);
                actual.Should().Be(expected);
            }
        }
    }
}
