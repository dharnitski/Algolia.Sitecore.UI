using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sitecore.FakeDb;
using AlgoliaUI.Code;
using FluentAssertions;

namespace AlgoliaUI.Tests
{
    [TestFixture]
    public class ItemExtensionsTests
    {
        [Test]
        public void ShouldDecodeField()
        {
            using (var db = new Db
            {
                new DbItem("Home") {{"Title", @"'&lt;div class=""facet - title""&gt;Materials&lt;/div class=""facet - title""&gt;'" } }
            })
            {
                var home = db.GetItem("/sitecore/content/home");

                var expected = home.GetFieldDecodedRawValue("Title");
                expected.ToHtmlString().Should().Be(@"'<div class=""facet - title"">Materials</div class=""facet - title"">'");
            }
        }
    }
}
