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
    public partial class MISRptYearly : System.Web.UI.Page
    {
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetUserAccess();

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

        private void PopulateControls()
        {
            CommonBLL commonBll = new CommonBLL();
            int roleId = UserBLL.GetLoggedInUserRoleId();

            GeneralFunctions.PopulateDropDownList<IProspect>(ddlPros, commonBll.GetActiveProspect(), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);

            if (roleId == (int)UserRole.SalesExecutive)
            {
                GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetLocationByUser(_userId), "Id", "Name", false);
            }
            else
            {
                GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetLocationByUser(_userId), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
            }

            PopulateYear();
        }

        private void PopulateYear()
        {
            for (int index = 2010; index < 2030; index++)
            {
                ddlYear.Items.Add(new ListItem(index.ToString(), index.ToString()));
            }

            ddlYear.SelectedValue = System.DateTime.Now.Year.ToString();
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();
            ICallDetail callDetail = new CallDetailEntity();
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "MISRptYearly", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "MISRptYearly.rdlc";

            BuildEntity(callDetail);
            IEnumerable<ICallDetail> lst = cls.GetYearlyMisReportData(Convert.ToInt32(ddlYear.SelectedValue), callDetail, Convert.ToChar(ddlParam.SelectedValue), _userId);
            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", lst));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ReportType", ddlParam.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("RptYear", ddlYear.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Prospect", ddlPros.SelectedItem.Text));
            rptViewer.LocalReport.Refresh();
        }

        private void BuildEntity(ICallDetail detail)
        {
            detail.LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
            detail.ProspectId = Convert.ToInt32(ddlPros.SelectedValue);
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
                            Response.Redirect("~/Unauthorized.aspx");
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