﻿using System;
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

        [Test]
        public void ShouldParseIndeses()
        {
            using (var db = new Db
            {
                new DbItem("Home") {{"Title",
                        @"ikea=Featured&ikea_price_asc=Price%20asc.&ikea_price_desc=Price%20desc." } }
            })
            {
                var home = db.GetItem("/sitecore/content/home");

                var expected = home.NameValuesToIndices("Title");
                expected.ToHtmlString().Should().Be("{ name: 'ikea', label: 'Featured' },\r\n{ name: 'ikea_price_asc', label: 'Price asc.' },\r\n{ name: 'ikea_price_desc', label: 'Price desc.' }");
            }
        }

        [Test]
        public void ShouldParseOptions()
        {
            using (var db = new Db
            {
                new DbItem("Home") {{"Title",
                        @"8=8%20per%20page&16=16%20per%20page&32=32%20per%20page" } }
            })
            {
                var home = db.GetItem("/sitecore/content/home");

                var expected = home.NameValuesToOptions("Title");
                expected.ToHtmlString().Should().Be("{ value: 8, label: '8 per page' },\r\n{ value: 16, label: '16 per page' },\r\n{ value: 32, label: '32 per page' }");
            }
        }
    }
}
