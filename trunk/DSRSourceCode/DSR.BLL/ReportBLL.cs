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
        public IEnumerable<ICallDetail> GetDailyCallData()
        {
            List<ICallDetail> lstRpt = ReportDAL.GetDailyCallData();
            //CallDetailEntity rpt = new CallDetailEntity();
            
            //rpt.Location = "Test";
            //lstRpt.Add(rpt);
            return lstRpt;
        }
    }
}
