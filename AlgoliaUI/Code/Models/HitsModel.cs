using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgoliaUI.Code.Models
{
    public class HitsModel: AlgoliaRenderingModel
    {
        public IHtmlString EmptyTemplate => GetPropertyValue("empty", "Empty Template", GetStringFieldValue);
        public IHtmlString ItemTemplate => GetPropertyValue("item", "Item Template", GetStringFieldValue);
        public IHtmlString HitsPerPage => GetPropertyValue("hitsPerPage", "Hits Per Page", GetFieldValue);
    }
}