using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace MPListControl.ControlTemplates.MPListControl
{
    public partial class Footer : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite siteCollection = new SPSite("http://sharepoint-test"))
                {
                    SPWeb rootweb = siteCollection.OpenWeb();
                    SPList list = rootweb.Lists.TryGetList("FooterText");
                    SPQuery q1 = new SPQuery();
                    string query = string.Format("<Where><IsNotNull><FieldRef Name='Title'/></IsNotNull></Where>", rootweb.Title);
                    q1.Query = query;
                    SPListItemCollection items = list.GetItems(q1);

                    StringWriter stringWriter = new StringWriter();
                    using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
                    {
                        if (items.Count > 0)
                        {
                            for (int i = 0; i < items.Count; i++)
                            {
                                writer.Write("<div class=\"col-sm-6\">");
                                writer.RenderBeginTag(HtmlTextWriterTag.P);
                                writer.Write(items[i]["Column1Text"]);
                                writer.RenderEndTag();
                                writer.Write("</div>");
                                writer.Write("<div class=\"col-sm-6\">");
                                writer.RenderBeginTag(HtmlTextWriterTag.P);
                                writer.Write(items[i]["Column2Text"]);
                                writer.RenderEndTag();
                                writer.Write("</div>");

                            }
                            FooterContent.Text = stringWriter.ToString();
                        }
                        else
                        {
                            FooterContent.Text = "List is Empty";
                        }
                    }
                }
            }
            );
        }
    }
}
