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
    public class SearchScriptModelTests
    {
        [Test]
        public void ShouldLoadAppIdFromDatasource()
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Application Id", "id"}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new SearchScriptModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.ApplicationId);
                actual.Should().Be("appId: 'id',");
            }
        }

        [Test]
        public void ShouldLoadAppIdFromSettings()
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Config Name", "MyTenant"}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                db.Configuration.Settings["Algolia.MyTenant.Application Id"] = "Id";

                var sut = new SearchScriptModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.ApplicationId);
                actual.Should().Be("appId: 'Id',");
            }
        }

        [Test]
        public void ShouldLoadAppKeyFromDatasource()
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Application Read Only Key", "id"}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new SearchScriptModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.ApplicationKey);
                actual.Should().Be("apiKey: 'id',");
            }
        }

        [Test]
        public void ShouldLoadAppKeyFromSettings()
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Config Name", "MyTenant"}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                db.Configuration.Settings["Algolia.MyTenant.Application Read Only Key"] = "Id";

                var sut = new SearchScriptModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.ApplicationKey);
                actual.Should().Be("apiKey: 'Id',");
            }
        }

        [Test]
        public void ShouldLoadIndexNameFromDatasource()
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Index Name", "id"}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                var sut = new SearchScriptModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.IndexName);
                actual.Should().Be("indexName: 'id',");
            }
        }

        [Test]
        public void ShouldLoadIndexNameFromSettings()
        {
            using (var db = new Db
            {
                new DbItem("home")
                {
                    {"Config Name", "MyTenant"}
                }
            })
            {
                var rendering = new Rendering
                {
                    Item = db.GetItem("/sitecore/content/home")
                };

                db.Configuration.Settings["Algolia.MyTenant.Index Name"] = "Id";

                var sut = new SearchScriptModel { Rendering = rendering };

                var actual = HttpUtility.HtmlEncode(sut.IndexName);
                actual.Should().Be("indexName: 'Id',");
            }
        }
    }
}
