﻿using System;
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
                return GetPropertyValue("item", "Item Template", GetFieldDecodedRawValue);
            }
        }

        private const string DefaultOperator = "or"; 

        public string Operator
        {
            get
            {
                string dropDownItemId = Item["Operator"];
                if (string.IsNullOrEmpty(dropDownItemId))
                    return DefaultOperator;
                var valueItem = Item.Database.GetItem(dropDownItemId);
                if (valueItem == null)
                {
                    Log.Error("Cannot find item " + dropDownItemId, this);
                    return DefaultOperator;
                }
                return valueItem["key"];
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