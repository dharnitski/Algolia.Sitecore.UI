using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AlgoliaUI.Code;
using AlgoliaUI.Code.Models;
using FluentAssertions;
using NUnit.Framework;
using Sitecore.FakeDb;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Tests.Models
{
    public class FakeModel: AlgoliaRenderingModel { }

    public class AlgoliaRenderingModelTests
    {

        [Test]
        public void CssClassesTest()
        {
            using (var db = new Db
            {
                new DbItem("Home")
                {
                    {
                        "Css Classes",
                        @"category=Cat&sub_category=Sub%20Cat&sub_sub_category=Sub%20Sub%20Category"
                    }
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new FakeModel{ Rendering = rendering };

                var expected = HttpUtility.HtmlEncode(sut.CssClasses);
                expected.Should().Be("cssClasses: {category: 'Cat',\r\nsub_category: 'Sub Cat',\r\nsub_sub_category: 'Sub Sub Category'},");
            }
        }
    }
}