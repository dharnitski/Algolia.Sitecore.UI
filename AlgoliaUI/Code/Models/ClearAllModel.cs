using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgoliaUI.Code.Models
{
    public class ClearAllModel : AlgoliaRenderingModel
    {
        public IHtmlString LinkTemplate => GetPropertyValue("link", "Link Template", GetStringFieldValue);

        public IHtmlString AutoHideContainer
            => GetPropertyValue("autoHideContainer", "Auto Hide Container", GetCheckBoxTrueFalse);
    }
}