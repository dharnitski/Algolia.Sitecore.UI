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
                return GetPropertyValue("item", "Item Template", GetStringFieldValue);
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
                return GetPropertyValue("header", "Header Template", GetStringFieldValue);
            }
        }

        /// <summary>
        /// renders JS property in format: name: fieldValue, 
        /// </summary>
        /// <param name="jsFieldName"></param>
        /// <param name="fieldName"></param>
        /// <param name="getFieldvalue"></param>
        /// <returns></returns>
        private IHtmlString GetPropertyValue(string jsFieldName, string fieldName, Func<string, string> getFieldvalue)
        {
            if (string.IsNullOrEmpty(Item[fieldName]))
                return new HtmlString(string.Empty);

            return new HtmlString($"{jsFieldName}: {getFieldvalue(fieldName)},");
        }


        /// <summary>
        /// makes sure that value is correct JS string. Could be 'value' or "value"
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private string GetStringFieldValue(string fieldName)
        {
            var value = GetFieldValue(fieldName);
            if (value == null)
                return null;

            if (value.StartsWith(@"""") || value.StartsWith("'"))
                return value;

            value = $"'{value}'";

            return value;
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