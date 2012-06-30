using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Reporting.WebForms;
using DSR.Utilities;

namespace DSR.Utilities.ReportManager
{
    /// <summary>
    /// Represents a class for managing reports.
    /// </summary>
    /// <createdby>Amit Kumar Chandra</createdby>
    /// <createddate>30/06/2012</createddate>
    public class LocalReportManager
    {
        #region Protected Member Variables

        protected List<DataTable> _lstReportDataSource = new List<DataTable>();
        protected List<DataTable> _lstSubReportDataSource = new List<DataTable>();
        protected List<ReportParameter> _lstReportParameter = new List<ReportParameter>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OCTMP.Utilities.ReportManager.LocalReportManager"/> class.
        /// </summary>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public LocalReportManager()
        {
            this.HasSubReport = false;
            this.RptViewer = new ReportViewer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OCTMP.Utilities.ReportManager.LocalReportManager"/> class.
        /// </summary>
        /// <param name="reportName">The Name of the report.</param>
        /// <param name="reportNamespace">The report namespace.</param>
        /// <param name="reportPath">The report path.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public LocalReportManager(string reportName, string reportNamespace, string reportPath)
        {
            this.HasSubReport = false;
            this.ReportNamespace = reportNamespace;
            this.ReportPath = reportPath;
            this.ReportName = reportName;
            this.RptViewer = new ReportViewer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OCTMP.Utilities.ReportManager.LocalReportManager"/> class.
        /// </summary>
        /// <param name="rptViewer">The <see cref="Microsoft.Reporting.WebForms.ReportViewer"/> object.</param>
        /// <param name="reportName">Name of the report.</param>
        /// <param name="reportNamespace">The report namespace.</param>
        /// <param name="reportPath">The report path.</param>
        public LocalReportManager(ReportViewer rptViewer, string reportName, string reportNamespace, string reportPath)
        {
            this.HasSubReport = false;
            this.ReportNamespace = reportNamespace;
            this.ReportPath = reportPath;
            this.ReportName = reportName;
            this.RptViewer = rptViewer;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the RPT viewer.
        /// </summary>
        /// <value>The RPT viewer.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public ReportViewer RptViewer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Report Path.
        /// </summary>
        /// <value>The report path.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public string ReportPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets name of the report.
        /// </summary>
        /// <value>The name of the report.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public string ReportName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the report namespace.
        /// </summary>
        /// <value>The report namespace.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public string ReportNamespace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Format of the report.
        /// </summary>
        /// <value>The report format.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public ReportFormat ReportFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether the report contains any subreport or not. The default value is false.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has sub report; otherwise, <c>false</c>.
        /// </value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public bool HasSubReport
        {
            get;
            set;
        }

        #endregion

        #region Private Event Handlers

        /// <summary>
        /// Handles SubreportProcessing event for the subreport.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Reporting.WebForms.SubreportProcessingEventArgs"/> instance containing the event data.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        //private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        //{
        //    for (int index = 0; index < _lstSubReportDataSource.Count; index++)
        //    {
        //        e.DataSources.Add(_lstSubReportDataSource[index]);
        //    }
        //}

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a Report Parameter with the specified Key and Value.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">Value of the parameter.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public void AddParameter(string paramName, string paramValue)
        {
            ReportParameter rptParam = new ReportParameter();
            rptParam.Name = paramName;
            rptParam.Values.Add(paramValue);
            rptParam.Visible = false;
            _lstReportParameter.Add(rptParam);
        }

        /// <summary>
        /// Adds report data source.
        /// </summary>
        /// <param name="dataSource">An instance of <see cref="Microsoft.Reporting.WebForms.ReportDataSource"/> containing the Data Source.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public void AddDataSource(DataTable dataSource)
        {
            _lstReportDataSource.Add(dataSource);
        }

        /// <summary>
        /// Adds report data source for subreport.
        /// </summary>
        /// <param name="dataSource">An instance of <see cref="Microsoft.Reporting.WebForms.ReportDataSource"/> containing the Data Source.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public void AddSubReportDataSource(DataTable dataSource)
        {
            _lstSubReportDataSource.Add(dataSource);
        }

        /// <summary>
        /// Export the Report in the specified format.
        /// </summary>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public void Export()
        {
            string fileName = String.Empty;
            string mimeType = String.Empty;
            string encoding = String.Empty;
            string fileNameExtension = String.Empty;
            string[] streams;
            string reportFormat;
            string contentType;

            Warning[] warnings;

            switch (ReportFormat)
            {
                case ReportFormat.PDF:
                    reportFormat = "PDF";
                    contentType = "application/pdf";
                    fileName = ReportName + ".pdf";
                    break;
                case ReportFormat.Excel:
                    reportFormat = "EXCEL";
                    contentType = "application/vnd.ms-excel";
                    fileName = ReportName + ".xls";
                    break;
                case ReportFormat.XML:
                    reportFormat = "XML";
                    contentType = "application/xml";
                    fileName = ReportName + ".xml";
                    break;
                default:
                    reportFormat = "PDF";
                    contentType = "application/pdf";
                    fileName = ReportName + ".pdf";
                    break;
            }

            this.RptViewer.LocalReport.ReportEmbeddedResource = this.ReportNamespace + "." + this.ReportName;
            this.RptViewer.LocalReport.ReportPath = this.ReportPath + "\\" + this.ReportName + ".rdlc";
            this.RptViewer.ProcessingMode = ProcessingMode.Local;
            this.RptViewer.LocalReport.DataSources.Clear();

            // Add DataSources for the report
            for (int index = 0; index < _lstReportDataSource.Count; index++)
            {
                this.RptViewer.LocalReport.DataSources.Add(new ReportDataSource(_lstReportDataSource[index].TableName, _lstReportDataSource[index]));
            }

            // Add Report Parameters
            for (int index = 0; index < _lstReportParameter.Count; index++)
            {
                this.RptViewer.LocalReport.SetParameters(new ReportParameter[] { _lstReportParameter[index] });
            }

            // If the report contains any subreport, then add corresponding datasources.
            //if (this.HasSubReport)
            //{
            //    this.RptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            //}

            this.RptViewer.LocalReport.Refresh();

            byte[] pdfContent = this.RptViewer.LocalReport.Render(reportFormat, null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();

            HttpContext.Current.Response.Buffer = true;
            Byte[] fileData = new Byte[2097151];
            HttpContext.Current.Response.ContentType = contentType;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(pdfContent);
            HttpContext.Current.Response.End();
        }

        public void Show()
        {
            this.RptViewer.ShowRefreshButton = false;
            this.RptViewer.ShowPrintButton = true;
            this.RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            this.RptViewer.LocalReport.DataSources.Clear();
            this.RptViewer.LocalReport.ReportPath = this.ReportPath + "\\" + this.ReportName + ".rdlc";

            // Add DataSources for the report
            for (int index = 0; index < _lstReportDataSource.Count; index++)
            {
                this.RptViewer.LocalReport.DataSources.Add(new ReportDataSource(_lstReportDataSource[index].TableName, _lstReportDataSource[index]));
            }

            // Add Report Parameters
            for (int index = 0; index < _lstReportParameter.Count; index++)
            {
                this.RptViewer.LocalReport.SetParameters(new ReportParameter[] { _lstReportParameter[index] });
            }

            this.RptViewer.LocalReport.Refresh();
        }

        #endregion
    }
}
