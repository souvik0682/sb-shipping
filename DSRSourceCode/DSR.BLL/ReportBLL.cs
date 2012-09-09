using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Entity;
using DSR.Common;
using DSR.DAL;

namespace DSR.BLL
{
    public class ReportBLL
    {
        public IEnumerable<ICallDetail> GetDailyCallData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetDailyCallData(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCallTypeWiseDailyData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCallTypeWiseDailyData(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCallDueData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCallDueData(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetSpecificCallTypeData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetSpecificCallTypeData(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCustomerWiseCallData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCustomerWiseCallData(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetLineWiseLocationSummary(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetLineWiseLocationSummary(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetLocationWiseLineSummary(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetLocationWiseLineSummary(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCustomerListWithLoc(ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCustomerListWithLoc(detail, System.DateTime.Now.Date, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCustomerWithCallDetail(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCustomerWithCallDetail(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetMisReportData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetMisReportData(fromDate, toDate, detail, userId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetYearlyMisReportData(int year, ICallDetail detail, char reportType, int userId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetYearlyMisReportData(year, detail, reportType, userId);
            return lstRpt;
        }
    }
}
