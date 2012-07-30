using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.Utilities.ReportManager;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data;
using DSR.BLL;
using DSR.Entity;
using DSR.Utilities;
using DSR.Common;

namespace DSR.WebApp.Reports
{
    public partial class CustListWithLoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateArea();
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

        private void PopulateArea()
        {
            GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, new CommonBLL().GetActiveArea(), "Id", "Name", Constants.DROPDOWNLIST_ALL_TEXT);
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
            rptViewer.LocalReport.Refresh();
        }
    }
}