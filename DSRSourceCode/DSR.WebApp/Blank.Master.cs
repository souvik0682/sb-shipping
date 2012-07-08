using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.Utilities;
using DSR.Common;
using DSR.Entity;

namespace DSR.WebApp
{
    public partial class Blank : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Clears the application cache.
            GeneralFunctions.ClearApplicationCache();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }
    }
}