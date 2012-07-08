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
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Clears the application cache.
            GeneralFunctions.ClearApplicationCache();

            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                SetAttributes(user);
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void lnkPwd_Click(object sender, EventArgs e)
        {

        }

        private void SetAttributes(IUser user)
        {
            lblUserName.Text = "Welcome " + user.UserFullName;
        }
    }
}