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
    [TestFixture]
    public class SearchBoxModelTests
    {
        [TestCase("", "")]
        [TestCase("hello &", "placeholder: 'hello &',")]
        public void LimitTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Placeholder", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new SearchBoxModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.Placeholder);
                actual.Should().Be(expected);
            }
        }

        [TestCase("1", "autofocus: true,")]
        [TestCase("0", "autofocus: false,")]
        [TestCase("", "")]
        public void GetCheckBoxTrueFalseTest(string value, string expected)
        {
            using (var db = new Db
            {
                new DbItem("Home")
                {
                    {
                        "Autofocus",
                        value
                    }
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new SearchBoxModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.Autofocus);
                expected.Should().Be(actual);
            }
        }
    }
}
