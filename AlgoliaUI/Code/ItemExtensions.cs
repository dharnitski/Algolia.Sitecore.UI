using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

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
    }
}