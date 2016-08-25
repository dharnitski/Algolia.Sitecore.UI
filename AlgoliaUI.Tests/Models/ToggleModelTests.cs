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
    public class ToggleModelTests
    {
        [TestCase("", "")]
        [TestCase(" ", "")]
        [TestCase("some-attribute", "attributeName: 'some-attribute',")]
        public void AttributeNameTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Attribute Name", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new ToggleModel {Rendering = rendering};

                var actual = HttpUtility.HtmlEncode(sut.AttributeName);
                actual.Should().Be(expected);
            }
        }

        [TestCase("", "")]
        [TestCase(" ", "")]
        [TestCase("some &", "label: 'some &',")]
        public void LabelTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Label", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new ToggleModel {Rendering = rendering};

                var actual = HttpUtility.HtmlEncode(sut.Label);
                actual.Should().Be(expected);
            }
        }

        [TestCase("", "")]
        [TestCase(" ", "")]
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

                var sut = new ToggleModel {Rendering = rendering};

                var actual = HttpUtility.HtmlEncode(sut.HeaderTemplate);
                actual.Should().Be(expected);
            }
        }

        [TestCase(@"on=true&off=false", "values: {on: 'true',\r\noff: 'false'},")]
        [TestCase(" ", "")]
        [TestCase("", "")]
        public void ValuesTest(string value, string expected)
        {
            using (var db = new Db
            {
                new DbItem("Home")
                {
                    {
                        "Values",
                        value
                    }
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new ToggleModel {Rendering = rendering};

                var actual = HttpUtility.HtmlEncode(sut.Values);
                actual.Should().Be(expected);
            }
        }
    }
}
