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

namespace DSR.WebApp.Reports
{
    public partial class LocWiseWiseLineSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GenerateReport();
        }

        private void GenerateReport()
        {
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "LocationWiseLineSummRpt", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            string rptName = "LocationWiseLineSummRpt.rdlc";
            //ReportBLL cls = new ReportBLL();
            //IEnumerable<DSR.Common.ICallDetail> lst = cls.GetDailyCallData();
            //rptViewer.Reset();
            //rptViewer.LocalReport.Dispose();
            //rptViewer.LocalReport.DataSources.Clear();
            //rptViewer.LocalReport.ReportPath = Server.MapPath("/" + ConfigurationManager.AppSettings["ReportPath"].ToString() + "/" + rptName);
            //rptViewer.LocalReport.DataSources.Add(new ReportDataSource("CallDetailEntity", lst));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("FromDate","14-May-2012"));
            //rptViewer.LocalReport.SetParameters(new ReportParameter("ToDate", "18-May-2012"));
            //rptViewer.LocalReport.Refresh();
        }
    }
}