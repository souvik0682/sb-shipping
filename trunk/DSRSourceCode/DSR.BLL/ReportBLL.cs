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
        public IEnumerable<ICallDetail> GetDailyCallData(DateTime fromDate, DateTime toDate, int callTypeId, int salesExecutiveId)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetDailyCallData(fromDate, toDate, callTypeId, salesExecutiveId);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCallDueData(DateTime fromDate, DateTime toDate)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCallDueData(fromDate, toDate);
            return lstRpt;
        }

        public IEnumerable<ICallDetail> GetCustomerListWithLoc(int areaId, DateTime currentDate)
        {
            List<ICallDetail> lstRpt = ReportDAL.GetCustomerListWithLoc(areaId, currentDate);
            return lstRpt;
        }

    }
}
