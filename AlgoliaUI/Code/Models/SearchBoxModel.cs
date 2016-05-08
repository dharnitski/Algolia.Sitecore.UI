using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgoliaUI.Code.Models
{
    public class SearchBoxModel: AlgoliaRenderingModel
    {
        public IHtmlString Placeholder => GetPropertyValue("placeholder", "Placeholder", GetStringFieldValue);

        public IHtmlString Autofocus => GetPropertyValue("autofocus", "Autofocus", GetCheckBoxTrueFalse);
    }
}