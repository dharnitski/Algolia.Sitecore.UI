using System;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Code.Models
{
    public class RefinementListModel: RenderingModel
    {
        public IHtmlString ItemTemplate
        {
            get
            {
                return GetPropertyValue("item", "Item Template", GetFieldValue);
            }
        }

        private const string DefaultOperator = "or"; 

        public IHtmlString Operator
        {
            get
            {
                return Item.ReferenceKey("Operator", DefaultOperator);
            } 
        }

        public IHtmlString HeaderTemplate
        {
            get
            {
                return GetPropertyValue("header", "Header Template", GetFieldValue);
            }
        }

        private IHtmlString GetPropertyValue(string jsFieldName, string fieldName, Func<string, string> getFieldvalue)
        {
            if (string.IsNullOrEmpty(Item[fieldName]))
                return new HtmlString(string.Empty);

            return new HtmlString($"{jsFieldName}: {getFieldvalue(fieldName)},");
        }

        private string GetFieldValue(string fieldName)
        {
            var value = Item.Fields[fieldName].Value;
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return value;
        }
    }
}