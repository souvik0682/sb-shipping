using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DSR.Common;
using DSR.DAL.DbManager;
using DSR.Entity;

namespace DSR.DAL
{
    public sealed class ReportDAL
    {
        private ReportDAL()
        {
        }

        public static List<ICallDetail> GetDailyCallData(DateTime fromDate, DateTime toDate, ICallDetail detail)
        {
            string strExecution = "[report].[uspGetDailyCall]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@SalesExecutiveId", detail.SalesPersionId);
                oDq.AddIntegerParam("@ProspectId", detail.ProspectId);
                oDq.AddIntegerParam("@CallTypeId", detail.CallTypeId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();

                    if (reader["LocId"] != DBNull.Value)
                    {
                        callDetail.LocationId = Convert.ToInt32(reader["LocId"]);
                        callDetail.LocationName = Convert.ToString(reader["LocName"]);
                    }

                    callDetail.ProspectFor = Convert.ToString(reader["ProspectName"]);
                    callDetail.CallDate = Convert.ToDateTime(reader["CallDate"]);
                    callDetail.GroupCompanyName = Convert.ToString(reader["GroupName"]);
                    callDetail.CallType = Convert.ToString(reader["CallType"]);

                    if (reader["NextCallOn"] != DBNull.Value)
                        callDetail.NextCallDate = Convert.ToDateTime(reader["NextCallOn"]);

                    callDetail.CallDetails = Convert.ToString(reader["Remarks"]);

                    if (reader["SalesPersionId"] != DBNull.Value)
                    {
                        callDetail.SalesPersionId = Convert.ToInt32(reader["SalesPersionId"]);
                        callDetail.SalesPersonName = Convert.ToString(reader["SalesPerson"]);
                    }

                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }

        public static List<ICallDetail> GetCallDueData(DateTime fromDate, DateTime toDate, ICallDetail detail)
        {
            string strExecution = "[report].[uspGetCallDue]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@SalesExecutiveId", detail.SalesPersionId);
                oDq.AddIntegerParam("@ProspectId", detail.ProspectId);
                oDq.AddIntegerParam("@CallTypeId", detail.CallTypeId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();

                    if (reader["LocId"] != DBNull.Value)
                    {
                        callDetail.LocationId = Convert.ToInt32(reader["LocId"]);
                        callDetail.LocationName = Convert.ToString(reader["LocName"]);
                    }

                    callDetail.ProspectFor = Convert.ToString(reader["ProspectName"]);
                    callDetail.CallDate = Convert.ToDateTime(reader["CallDate"]);
                    callDetail.GroupCompanyName = Convert.ToString(reader["GroupName"]);
                    callDetail.CallType = Convert.ToString(reader["CallType"]);

                    if (reader["NextCallOn"] != DBNull.Value)
                        callDetail.NextCallDate = Convert.ToDateTime(reader["NextCallOn"]);

                    callDetail.CallDetails = Convert.ToString(reader["Remarks"]);

                    if (reader["SalesPersionId"] != DBNull.Value)
                    {
                        callDetail.SalesPersionId = Convert.ToInt32(reader["SalesPersionId"]);
                        callDetail.SalesPersonName = Convert.ToString(reader["SalesPerson"]);
                    }

                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }

        public static List<ICallDetail> GetCustomerListWithLoc(int areaId, DateTime currentDate)
        {
            string strExecution = "[report].[uspGetCustomerListWithLoc]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AreaId", areaId);
                oDq.AddDateTimeParam("@CurrentDate", currentDate);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();
                    callDetail.AreaName = Convert.ToString(reader["AreaName"]);
                    callDetail.GroupCompanyName = Convert.ToString(reader["GroupName"]);
                    callDetail.Address = Convert.ToString(reader["Address"]);
                    callDetail.Contact = Convert.ToString(reader["Contact"]);
                    callDetail.Profile = Convert.ToString(reader["CustomerProfile"]);
                    callDetail.TEU = Convert.ToInt32(reader["TEU"]);
                    callDetail.SalesPersonName = Convert.ToString(reader["SalesPerson"]);
                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }
    }
}
