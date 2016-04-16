using System;
using System.Web;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Code.Models
{
    public class RefinementListModel: RenderingModel
    {
        public IHtmlString ItemTemplate
        {
            get
            {
                return GetPropertyValue("item", "Item Template", GetFieldDecodedRawValue);
            }
        }

        public IHtmlString HeaderTemplate
        {
            get
            {
                return GetPropertyValue("header", "Header Template", GetFieldDecodedRawValue);
            }
        }

        private IHtmlString GetPropertyValue(string jsFieldName, string fieldName, Func<string, string> getFieldvalue)
        {
            if (string.IsNullOrEmpty(Item[fieldName]))
                return new HtmlString(string.Empty);

            return new HtmlString($"{jsFieldName}: {getFieldvalue(fieldName)},");
        }

        public string GetFieldDecodedRawValue(string fieldName)
        {
            var value = Item.Fields[fieldName].Value;
            if (string.IsNullOrWhiteSpace(value))
                return null;
            var decodedValue = HttpUtility.HtmlDecode(value);

            return decodedValue;
        }
    }
}