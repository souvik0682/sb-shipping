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
using DSR.Utilities.ResourceManager;

namespace DSR.WebApp.Reports
{
    public partial class CustListWithLoc : System.Web.UI.Page
    {
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;
        private int _roleId = 0;
        private int _locId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            SetUserAccess();
            SetAttributes();

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

        //protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlLoc.SelectedValue == "0")
        //        GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetActiveArea(), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
        //    else
        //        GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetAreaByLocation(Convert.ToInt32(ddlLoc.SelectedValue)), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
        //}

        //private void SetAttributes()
        //{
        //    if (!IsPostBack)
        //    {

        //    }
        //}

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                txtWMEArea.WatermarkText = ResourceManager.GetStringWithoutName("ERR00016");
            }
        }

        private void PopulateControls()
        {
            CommonBLL commonBll = new CommonBLL();
            int roleId = UserBLL.GetLoggedInUserRoleId();

            if (roleId == (int)UserRole.SalesExecutive || roleId == (int)UserRole.Manager)
            {
                GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetLocationByUser(_userId), "Id", "Name", false);
                //GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetAreaByLocation(_locId), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
            }
            else
            {
                GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetLocationByUser(_userId), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
                //GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetActiveArea(), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
            }

            if (roleId == (int)UserRole.SalesExecutive)
            {
                GeneralFunctions.PopulateDropDownList<IUser>(ddlSales, commonBll.GetSalesExecutive(_userId), "Id", "UserFullName", false);
            }
            else
            {
                GeneralFunctions.PopulateDropDownList<IUser>(ddlSales, commonBll.GetSalesExecutive(_userId), "Id", "UserFullName", Constants.DROPDOWNLIST_ALL_TEXT);
            }
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();
            ICallDetail callDetail = new CallDetailEntity();
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "CustomerListWithLoc", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "CustomerListWithLoc.rdlc";

            BuildEntity(callDetail);
            IEnumerable<ICallDetail> lst = cls.GetCustomerListWithLoc(callDetail, _userId);
            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", lst));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));

            if (txtArea.Text != ResourceManager.GetStringWithoutName("ERR00016"))
                rptViewer.LocalReport.SetParameters(new ReportParameter("AreaName", txtArea.Text));
            else
                rptViewer.LocalReport.SetParameters(new ReportParameter("AreaName", txtArea.Text));

            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("SalesPerson", ddlSales.SelectedItem.Text));
            rptViewer.LocalReport.Refresh();
        }

        private void BuildEntity(ICallDetail detail)
        {
            detail.LocationId = Convert.ToInt32(ddlLoc.SelectedValue);

            if (txtArea.Text != ResourceManager.GetStringWithoutName("ERR00016"))
                detail.AreaName = txtArea.Text.Trim();
            else
                detail.AreaName = string.Empty;

            //detail.AreaId = Convert.ToInt32(ddlArea.SelectedValue);
            detail.SalesPersionId = Convert.ToInt32(ddlSales.SelectedValue);
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            IUser user = new UserBLL().GetUser(_userId);

            if (!ReferenceEquals(user, null))
            {
                if (!ReferenceEquals(user.UserRole, null))
                {
                    _roleId = user.UserRole.Id;
                }

                if (!ReferenceEquals(user.UserLocation, null))
                {
                    _locId = user.UserLocation.Id;
                }
            }
        }

        private void SetUserAccess()
        {
            if (_userId > 0)
            {
                switch (_roleId)
                {
                    case (int)UserRole.Admin:
                    case (int)UserRole.Management:
                    case (int)UserRole.Manager:
                        break;
                    case (int)UserRole.SalesExecutive:
                        //ddlLoc.Enabled = false;
                        ddlSales.Enabled = false;
                        Response.Redirect("~/Unauthorized.aspx");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}