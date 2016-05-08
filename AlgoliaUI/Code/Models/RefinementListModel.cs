using System;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Code.Models
{
    public class RefinementListModel: AlgoliaRenderingModel
    {
        private const string DefaultOperator = "or";

        public IHtmlString ItemTemplate => GetPropertyValue("item", "Item Template", GetStringFieldValue);

        public IHtmlString Operator => ReferenceKey("Operator", DefaultOperator);

        public IHtmlString Limit => GetPropertyValue("limit", "Limit", GetFieldValue);

        public IHtmlString HeaderTemplate => GetPropertyValue("header", "Header Template", GetStringFieldValue);
    }
}