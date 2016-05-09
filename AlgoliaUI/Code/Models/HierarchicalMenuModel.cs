using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgoliaUI.Code.Models
{
    public class HierarchicalMenuModel : AlgoliaRenderingModel
    {
        public IHtmlString HeaderTemplate => GetPropertyValue("header", "Header Template", GetStringFieldValue);

        public IHtmlString ItemTemplate => GetPropertyValue("item", "Item Template", GetStringFieldValue);

        public IHtmlString SortBy => GetPropertyValue("sortBy", "Sort By", GetFieldValue);
    }
}