using System.Web;

namespace AlgoliaUI.Code.Models
{
    public class ToggleModel: AlgoliaRenderingModel
    {
        public IHtmlString AttributeName => GetPropertyValue("attributeName", "Attribute Name", GetStringFieldValue);

        public IHtmlString Label => GetPropertyValue("label", "Label", GetStringFieldValue);

        public IHtmlString HeaderTemplate => GetPropertyValue("header", "Header Template", GetStringFieldValue);

        public IHtmlString Values => GetObjectValue("values", "Values", NameValuesToCssClasses);
    }
}