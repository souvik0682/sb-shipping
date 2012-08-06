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
        public IEnumerable<ICallDetail> GetDailyCallData(DateTime fromDate, DateTime toDate, ICallDetail detail)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetDailyCallData(fromDate, toDate, detail);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCallDueData(DateTime fromDate, DateTime toDate, ICallDetail detail)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCallDueData(fromDate, toDate, detail);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCustomerListWithLoc(int areaId, DateTime currentDate)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCustomerListWithLoc(areaId, currentDate);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetLocationWiseLineSummary(DateTime fromDate, DateTime toDate, ICallDetail detail)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetLocationWiseLineSummary(fromDate, toDate, detail);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetLineWiseLocationSummary(DateTime fromDate, DateTime toDate, ICallDetail detail)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetLineWiseLocationSummary(fromDate, toDate, detail);
            return lstRpt;
        }
    }
}
