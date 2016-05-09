using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgoliaUI.Code.Models
{
    public class MenuModel : AlgoliaRenderingModel
    {
        public IHtmlString HeaderTemplate => GetPropertyValue("header", "Header Template", GetStringFieldValue);

        public IHtmlString AttributeName => GetPropertyValue("attributeName", "Attribute Name", GetStringFieldValue);

        public IHtmlString Limit => GetPropertyValue("limit", "Limit", GetFieldValue);
    }
}
