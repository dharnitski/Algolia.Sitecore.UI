using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgoliaUI.Code.Models
{
    public class StarRatingModel: AlgoliaRenderingModel
    {
        public IHtmlString HeaderTemplate => GetPropertyValue("header", "Header Template", GetStringFieldValue);
    }
}
