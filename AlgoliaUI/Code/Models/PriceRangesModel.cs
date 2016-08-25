using System.Web;

namespace AlgoliaUI.Code.Models
{
    public class PriceRangesModel: AlgoliaRenderingModel
    {
        public IHtmlString HeaderTemplate => GetPropertyValue("header", "Header Template", GetStringFieldValue);

        public IHtmlString AttributeName => GetPropertyValue("attributeName", "Attribute Name", GetStringFieldValue);
    }
}
