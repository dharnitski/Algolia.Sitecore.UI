using System;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;

namespace AlgoliaUI.Code.Models
{
    public class NumericSelectorModel: RenderingModel
    {
        private const string DefaultOperator = ">=";

        public IHtmlString Operator
        {
            get
            {
                return Item.ReferenceKey("Operator", DefaultOperator);
            } 
        }

        public IHtmlString Options
        {
            get
            {
                return Item.NameValuesToOptions("Options");
            }
        }
    }
}