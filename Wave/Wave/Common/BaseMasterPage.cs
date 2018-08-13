using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Wave.Common
{
    public class BaseMasterPage : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "GlobalNamespace",
                       "<script language='javascript'>var Wave = Wave || {};</script>", false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public static string ToAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}",
                url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }
        public void RegisterClientScriptVariable(string varName, string varValue)
        {
            string prefix = "Wave.";

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(prefix + varName))
            {
                StringBuilder jsCode = new StringBuilder();

                jsCode.Append("<" + "script language=\"javascript\">\n");

                jsCode.AppendFormat(" {0} = '{1}'; ", prefix + varName, varValue);

                jsCode.Append("\n<" + "/script>");

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), prefix + varName,
                    jsCode.ToString(), false);

            }
        }
    }
}