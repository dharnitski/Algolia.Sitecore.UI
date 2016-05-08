using System;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data.Items;

namespace AlgoliaUI.Code.Models
{
    public class SearchScriptModel: AlgoliaRenderingModel
    {
        public IHtmlString UrlSync => GetPropertyValue("urlSync", "Url Sync", GetCheckBoxTrueFalse);

        /// <summary>
        /// Index Configuration (App, key, Index Name taken from settings if ConfigName defined)
        /// settings key format is Algolia.ConfifName.XXX
        /// Otherwise it is taken from Component Datasource
        /// </summary>
        private string ConfigName => GetFieldValue("Config Name");

        private bool UseComponentConfig => string.IsNullOrEmpty(ConfigName);

        public IHtmlString ApplicationId => GetPropertyStringValueWithFallback("appId", "Application Id", GetFieldValueWithSettingsFallback);

        public IHtmlString ApplicationKey => GetPropertyStringValueWithFallback("apiKey", "Application Read Only Key", GetFieldValueWithSettingsFallback);

        public IHtmlString IndexName => GetPropertyStringValueWithFallback("indexName", "Index Name", GetFieldValueWithSettingsFallback);

        public IHtmlString SyncUrl => GetPropertyValue("syncUrl", "Sync Url", GetCheckBoxTrueFalse);

        public bool RenderResources => bool.Parse(GetCheckBoxTrueFalse("Render Resources"));

        private IHtmlString GetPropertyStringValueWithFallback(string jsFieldName, string fieldName, Func<string, string> getFieldvalue)
        {
            return new HtmlString($"{jsFieldName}: {getFieldvalue(fieldName)},");
        }

        private string GetFieldValueWithSettingsFallback(string fieldName)
        {
            if (UseComponentConfig)
            {
                return GetStringFieldValue(fieldName);
            }
            else
            {
                var setttingKey = $"Algolia.{ConfigName}.{fieldName}";
                var settingsValue = Settings.GetSetting(setttingKey);
                return $"'{settingsValue}'";
            }
        }
    }
}