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

            if (!Request.Path.Contains("ChangePassword.aspx"))
            {
                if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
                {
                    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                    if (ReferenceEquals(user, null) || user.Id == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }

                    SetAttributes(user);
                    ShowMenu(user);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
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

        private void ShowMenu(IUser user)
        {
            liSpecificCallType.Style["display"] = "none";
            liMaster.Style["display"] = "none";

            liUserMst.Style["display"] = "none";
            liLocMst.Style["display"] = "none";
            liAreaMst.Style["display"] = "none";
            liGrMst.Style["display"] = "none";
            liCustMst.Style["display"] = "none";
            liCustAssign.Style["display"] = "none";
            liLineWiseLoc.Style["display"] = "none";
            liLocWiseLine.Style["display"] = "none";
            liCustList.Style["display"] = "none";
            liCustCall.Style["display"] = "none";
            liMisRpt.Style["display"] = "none";
            liMisRptYearly.Style["display"] = "none";

            switch (user.UserRole.Id)
            {
                case (int)UserRole.Admin:
                    liMaster.Style["display"] = "";
                    liUserMst.Style["display"] = "";
                    liLocMst.Style["display"] = "";
                    liAreaMst.Style["display"] = "";
                    liGrMst.Style["display"] = "";
                    liCustMst.Style["display"] = "";
                    liCustAssign.Style["display"] = "";
                    liLineWiseLoc.Style["display"] = "";
                    liLocWiseLine.Style["display"] = "";
                    liCustList.Style["display"] = "";
                    liCustCall.Style["display"] = "";
                    liMisRpt.Style["display"] = "";
                    liMisRptYearly.Style["display"] = "";
                    break;
                case (int)UserRole.Management:
                    liMaster.Style["display"] = "";
                    liCustMst.Style["display"] = "";
                    liCustAssign.Style["display"] = "";
                    liLineWiseLoc.Style["display"] = "";
                    liLocWiseLine.Style["display"] = "";
                    liCustList.Style["display"] = "";
                    liCustCall.Style["display"] = "";
                    liMisRpt.Style["display"] = "";
                    liMisRptYearly.Style["display"] = "";
                    break;
                case (int)UserRole.Manager:
                    liMaster.Style["display"] = "";
                    liCustMst.Style["display"] = "";
                    liCustAssign.Style["display"] = "";
                    liLineWiseLoc.Style["display"] = "";
                    liLocWiseLine.Style["display"] = "";
                    liCustList.Style["display"] = "";
                    liCustCall.Style["display"] = "";
                    liMisRpt.Style["display"] = "";
                    liMisRptYearly.Style["display"] = "";
                    break;
                case (int)UserRole.SalesExecutive:
                    liMaster.Style["display"] = "";
                    liCustMst.Style["display"] = "";
                    break;
                default:
                    break;
            }
        }
    }
}