using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.BLL;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities;
using DSR.Utilities.ReportManager;
using Microsoft.Reporting.WebForms;

namespace DSR.WebApp.Reports
{
    public partial class CustListWithLoc : System.Web.UI.Page
    {
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetUserAccess();
            //SetAttributes();

            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateReport();
            }
            catch (Exception ex)
            {
                GeneralFunctions.RegisterErrorAlertScript(this, ex.Message);
            }
        }

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLoc.SelectedValue == "0")
                GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetActiveArea(), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
            else
                GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetAreaByLocation(Convert.ToInt32(ddlLoc.SelectedValue)), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
        }

        //private void SetAttributes()
        //{
        //    if (!IsPostBack)
        //    {

        //    }
        //}

        private void PopulateControls()
        {
            CommonBLL commonBll = new CommonBLL();
            int roleId = UserBLL.GetLoggedInUserRoleId();

            if (roleId == (int)UserRole.SalesExecutive)
            {
                GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetLocationByUser(_userId), "Id", "Name", false);
                GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetActiveArea(), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
            }
            else
            {
                GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetLocationByUser(_userId), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
                GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetActiveArea(), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
            }
        }

        private void GenerateReport()
        {
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "CustomerListWithLoc", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "CustomerListWithLoc.rdlc";

            ReportBLL cls = new ReportBLL();
            IEnumerable<ICallDetail> lst = cls.GetCustomerListWithLoc(Convert.ToInt32(ddlArea.SelectedValue), System.DateTime.Now.Date);
            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = Server.MapPath("/" + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName);
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", lst));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("AreaName", ddlArea.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            rptViewer.LocalReport.Refresh();
        }

        private void SetUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null) && user.Id > 0)
                {
                    _userId = user.Id;

                    switch (user.UserRole.Id)
                    {
                        case (int)UserRole.Admin:
                        case (int)UserRole.Management:
                        case (int)UserRole.Manager:
                            break;
                        case (int)UserRole.SalesExecutive:
                            ddlLoc.Enabled = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}