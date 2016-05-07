using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Sitecore;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace AlgoliaUI.Code.Controls
{
    public class AlgoliaTemplate : Input
    {
        protected override void DoRender(HtmlTextWriter output)
        {
            if (Height.Value < 100)
                Height = 100;

            this.SetWidthAndHeightStyle();
            output.Write("<textarea" + ControlAttributes + ">" + Value);
            this.RenderChildren(output);
            output.Write("</textarea>");
        }

        protected override bool LoadPostData(string value)
        {
            value = StringUtil.GetString(new string[1]
            {
                value
            });
            if (this.Value == value)
                return false;
            this.Value = value;
            this.SetModified();
            return true;
        }

        public override string Value
        {
            get
            {
                return this.GetViewStateString("Value");
            }
            set
            {
                if (value == this.Value)
                    return;
                this.SetViewStateString("Value", value);
                SheerResponse.SetAttribute(this.ID, "value", value);
            }
        }

        protected override void SetModified()
        {
            base.SetModified();
            if (!TrackModified)
                return;
            Sitecore.Context.ClientPage.Modified = true;
        }
    }
}