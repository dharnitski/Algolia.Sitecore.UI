using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;

namespace AlgoliaUI.Code.FieldTypes
{
    public class AlgoliaTemplateField : CustomField
    {
        public AlgoliaTemplateField(Field innerField) : base(innerField)
        {
        }

        public AlgoliaTemplateField(Field innerField, string runtimeValue) : base(innerField, runtimeValue)
        {
        }

        public static implicit operator AlgoliaTemplateField(Field field)
        {
            return field != null ? new AlgoliaTemplateField(field) : null;
        }
    }
}