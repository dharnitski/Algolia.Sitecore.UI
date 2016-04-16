using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace AlgoliaUI.Code
{
    public static class ItemExtensions
    {
        public static IHtmlString GetFieldDecodedRawValue(this Item item, string fieldName)
        {
            var value = item.Fields[fieldName].Value;
            if (string.IsNullOrWhiteSpace(value))
                return null;
            var decodedValue = HttpUtility.HtmlDecode(value);

            return new HtmlString(decodedValue);
        }

        public static string GetCheckBoxTrueFalse(this Item item, string fieldName)
        {
            var value = item.Fields[fieldName].Value;
            if (string.IsNullOrWhiteSpace(value))
                return "false";
            if (value == "1")
                return "true";
            else return "false";
        }

        /// <summary>
        /// sample output
        /// { name: 'ikea', label: 'Featured' },
        /// { name: 'ikea_price_asc', label: 'Price asc.' },
        /// { name: 'ikea_price_desc', label: 'Price desc.' }
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IHtmlString NameValuesToIndices(this Item item, string fieldName)
        {
            return NameValuesToJsArray(item, fieldName, (key, value) => $"{{ name: '{key}', label: '{value}' }},");
        }


        /// <summary>
        /// sample result
        /// list: 'nav nav-list',
        /// count: 'badge pull-right',
        /// active: 'active'
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IHtmlString NameValuesToCssClasses(this Item item, string fieldName)
        {
            return NameValuesToJsArray(item, fieldName, (key, value) => $"{key}: '{value}',");
        }

        /// <summary>
        /// sample output
        /// { value: 8, label: '8 per page' },
        /// { value: 16, label: '16 per page' },
        /// { value: 32, label: '32 per page' }
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IHtmlString NameValuesToOptions(this Item item, string fieldName)
        {
            return NameValuesToJsArray(item, fieldName, (key, value) => $"{{ value: {key}, label: '{value}' }},");
        }

        /// <summary>
        /// Get Key from Reference Item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IHtmlString ReferenceKey(this Item item, string fieldName, string defaultValue)
        {
           
                string dropDownItemId = item[fieldName];
                if (string.IsNullOrEmpty(dropDownItemId))
                    return new HtmlString(defaultValue);
                var valueItem = item.Database.GetItem(dropDownItemId);
                if (valueItem == null)
                {
                    Log.Error("Cannot find item " + dropDownItemId, item);
                    return new HtmlString(defaultValue);
                }
                return new HtmlString(valueItem["key"]);
        }


        /// <summary>
        /// Sample Data - category=Cat&sub_category=Sub%20Cat&sub_sub_category=Sub%20Sub%20Category
        /// Result - 'category', 'sub_category', 'sub_sub_category'
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IHtmlString NameValuesToAttributes(this Item item, string fieldName)
        {
            var fieldValue = item.Fields[fieldName].Value;
            if (string.IsNullOrWhiteSpace(fieldValue))
                return null;

            NameValueCollection nameValueCollection = Sitecore.Web.WebUtil.ParseUrlParameters(fieldValue);

            var sb = new StringBuilder();

            foreach (string key in nameValueCollection)
            {
                sb.Append($"'{key}', ");
            }
            var result = sb.ToString();

            if (result.Length > 2)
                result = result.Substring(0, result.Length - 2);

            return new HtmlString(result);
        }

        internal static IHtmlString NameValuesToJsArray(Item item, string fieldName,
            Func<string, string, string> formatLineFunc)
        {
            var fieldValue = item.Fields[fieldName].Value;
            if (string.IsNullOrWhiteSpace(fieldValue))
                return null;

            NameValueCollection nameValueCollection = Sitecore.Web.WebUtil.ParseUrlParameters(fieldValue);

            var sb = new StringBuilder();

            foreach (string key in nameValueCollection)
            {
                var value = nameValueCollection[key];
                var line = formatLineFunc(key, value);
                sb.AppendLine(line);
            }
            var result = sb.ToString();
            //remove last comma
            if (!string.IsNullOrEmpty(result))
                result = result.Substring(0, result.Length - Environment.NewLine.Length - 1);

            return new HtmlString(result);
        }


    }
}