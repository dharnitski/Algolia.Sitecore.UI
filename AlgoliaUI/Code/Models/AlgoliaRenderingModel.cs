using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Code.Models
{
    public abstract class AlgoliaRenderingModel: RenderingModel
    {
        /// <summary>
        /// renders JS property in format: name: fieldValue, 
        /// </summary>
        /// <param name="jsFieldName"></param>
        /// <param name="fieldName"></param>
        /// <param name="getFieldvalue"></param>
        /// <returns></returns>
        protected IHtmlString GetPropertyValue(string jsFieldName, string fieldName, Func<string, string> getFieldvalue)
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
        protected string GetStringFieldValue(string fieldName)
        {
            var value = GetFieldValue(fieldName);
            if (value == null)
                return null;

            if (value.StartsWith(@"""") || value.StartsWith("'"))
                return value;

            value = $"'{value}'";

            return value;
        }

        protected string GetFieldValue(string fieldName)
        {
            var field = Item.Fields[fieldName];
            if (field == null)
            {
                Log.Warn($"Cannot Find field '{fieldName}' in item {Item.Paths.FullPath}", this);
                return null;
            }

            var value = Item.Fields[fieldName].Value;
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return value;
        }

        protected IHtmlString ReferenceKey(string fieldName, string defaultValue)
        {
            return Item.ReferenceKey(fieldName, defaultValue);
        }

        protected string GetCheckBoxTrueFalse(string fieldName)
        {
            return Item.GetCheckBoxTrueFalse(fieldName);
        }


    }
}