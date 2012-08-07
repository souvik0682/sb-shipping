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
    public partial class CallTypeWiseDailyRpt : System.Web.UI.Page
    {
        #region Private Member Variables

        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
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
                string message = string.Empty;

                if (ValidateData(out message))
                {
                    GenerateReport();
                }
                else
                {
                    GeneralFunctions.RegisterAlertScript(this, message);
                }
            }
            catch (Exception ex)
            {
                GeneralFunctions.RegisterErrorAlertScript(this, ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                cbeFromDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                cbeToDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtFromDt.Text = System.DateTime.Now.Date.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                txtToDt.Text = System.DateTime.Now.Date.ToString(ConfigurationManager.AppSettings["DateFormat"]);
            }
        }

        private bool ValidateData(out string message)
        {
            bool isValid = true;
            int slNo = 1;
            message = GeneralFunctions.FormatAlertMessage("Please correct the following errors:");

            if (string.IsNullOrEmpty(txtFromDt.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please enter from date");
                slNo++;
            }

            if (string.IsNullOrEmpty(txtToDt.Text))
            {
                isValid = false;
                message += GeneralFunctions.FormatAlertMessage(slNo, "Please enter to date");
                slNo++;
            }

            if (!isValid)
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }

            return isValid;
        }

        private void PopulateControls()
        {
            CommonBLL commonBll = new CommonBLL();
            int roleId = UserBLL.GetLoggedInUserRoleId();

            GeneralFunctions.PopulateDropDownList<ICallType>(ddlType, commonBll.GetActiveCallType(), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);

            if (roleId == (int)UserRole.SalesExecutive)
            {
                GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetLocationByUser(_userId), "Id", "Name", false);
                GeneralFunctions.PopulateDropDownList<IUser>(ddlSales, commonBll.GetSalesExecutive(_userId), "Id", "UserFullName", false);
            }
            else
            {
                GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetLocationByUser(_userId), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
                GeneralFunctions.PopulateDropDownList<IUser>(ddlSales, commonBll.GetSalesExecutive(_userId), "Id", "UserFullName", Constants.DROPDOWNLIST_ALL_TEXT);
            }
        }

        private void GenerateReport()
        {
            ReportBLL cls = new ReportBLL();
            ICallDetail callDetail = new CallDetailEntity();
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "CallTypeWiseDailyRpt", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "CallTypeWiseDailyRpt.rdlc";
            DateTime fromDate;
            DateTime toDate;
            fromDate = Convert.ToDateTime(txtFromDt.Text, _culture);
            toDate = Convert.ToDateTime(txtToDt.Text, _culture);
            BuildEntity(callDetail);
            IEnumerable<ICallDetail> lst = cls.GetDailyCallData(fromDate, toDate, callDetail);

            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName;
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", lst));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            rptViewer.LocalReport.SetParameters(new ReportParameter("FromDate", txtFromDt.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("ToDate", txtToDt.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Location", ddlLoc.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("SalesPerson", ddlSales.SelectedItem.Text));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CallType", ddlType.SelectedItem.Text));
            rptViewer.LocalReport.Refresh();
        }

        private void BuildEntity(ICallDetail detail)
        {
            detail.LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
            detail.SalesPersionId = Convert.ToInt32(ddlSales.SelectedValue);
            detail.ProspectId = 0;
            detail.CallTypeId = Convert.ToInt32(ddlType.SelectedValue);
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
                            ddlSales.Enabled = false;
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

        #endregion
    }
}