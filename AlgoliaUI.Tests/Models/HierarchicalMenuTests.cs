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
    public class HierarchicalMenuModelTests
    {
        public void SortByTest()
        {


        }

        [TestCase("", "")]
        [TestCase("['name:asc']", "sortBy: ['name:asc'],")]
        public void SortByTest(string input, string expected)
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Sort By", input}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new HierarchicalMenuModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.SortBy);
                actual.Should().Be(expected);
            }
        }

    }
}
