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

        public static List<ICallDetail> GetDailyCallData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
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
                oDq.AddIntegerParam("@UserId", userId);

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

        public static List<ICallDetail> GetCallTypeWiseDailyData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            string strExecution = "[report].[uspGetCallTypeWiseDailyData]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@SalesExecutiveId", detail.SalesPersionId);
                oDq.AddIntegerParam("@ProspectId", detail.ProspectId);
                oDq.AddIntegerParam("@CallTypeId", detail.CallTypeId);
                oDq.AddIntegerParam("@UserId", userId);

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

        public static List<ICallDetail> GetCallDueData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
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
                oDq.AddIntegerParam("@UserId", userId);

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

        public static List<ICallDetail> GetSpecificCallTypeData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            string strExecution = "[report].[uspGetSpecificCallTypeData]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@SalesExecutiveId", detail.SalesPersionId);
                oDq.AddIntegerParam("@ProspectId", detail.ProspectId);
                oDq.AddIntegerParam("@CallTypeId", detail.CallTypeId);
                oDq.AddIntegerParam("@UserId", userId);

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

        public static List<ICallDetail> GetCustomerWiseCallData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            string strExecution = "[report].[uspGetCustomerWiseCallData]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@SalesExecutiveId", detail.SalesPersionId);
                oDq.AddIntegerParam("@ProspectId", detail.ProspectId);
                oDq.AddIntegerParam("@CallTypeId", detail.CallTypeId);
                oDq.AddIntegerParam("@UserId", userId);

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

        public static List<ICallDetail> GetLineWiseLocationSummary(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            string strExecution = "[report].[uspGetLineWiseLocationSummary]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@ProspectId", detail.ProspectId);
                oDq.AddIntegerParam("@UserId", userId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();

                    callDetail.LocationId = Convert.ToInt32(reader["LocId"]);
                    callDetail.LocationName = Convert.ToString(reader["LocName"]);
                    callDetail.ProspectId = Convert.ToInt32(reader["ProspectId"]);
                    callDetail.ProspectFor = Convert.ToString(reader["ProspectName"]);
                    callDetail.TEU = Convert.ToInt32(reader["TEU"]);
                    callDetail.FEU = Convert.ToInt32(reader["FEU"]);
                    callDetail.TEUActual = Convert.ToInt32(reader["TEUActual"]);
                    callDetail.FEUActual = Convert.ToInt32(reader["FEUActual"]);

                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }

        public static List<ICallDetail> GetLocationWiseLineSummary(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            string strExecution = "[report].[uspGetLocationWiseLineSummary]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@ProspectId", detail.ProspectId);
                oDq.AddIntegerParam("@UserId", userId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();

                    callDetail.LocationId = Convert.ToInt32(reader["LocId"]);
                    callDetail.LocationName = Convert.ToString(reader["LocName"]);
                    callDetail.ProspectId = Convert.ToInt32(reader["ProspectId"]);
                    callDetail.ProspectFor = Convert.ToString(reader["ProspectName"]);
                    callDetail.SalesPersonName = Convert.ToString(reader["SalesPerson"]);
                    callDetail.TEU = Convert.ToInt32(reader["TEU"]);
                    callDetail.FEU = Convert.ToInt32(reader["FEU"]);
                    callDetail.TEUActual = Convert.ToInt32(reader["TEUActual"]);
                    callDetail.FEUActual = Convert.ToInt32(reader["FEUActual"]);

                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }

        public static List<ICallDetail> GetCustomerListWithLoc(ICallDetail detail, DateTime currDate, int userId)
        {
            string strExecution = "[report].[uspGetCustomerListWithLoc]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddVarcharParam("@AreaName", 50, detail.AreaName);
                oDq.AddIntegerParam("@SalesExecutiveId", detail.SalesPersionId);
                oDq.AddDateTimeParam("@CurrDate", currDate);
                oDq.AddIntegerParam("@UserId", userId);

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

        public static List<ICallDetail> GetCustomerWithCallDetail(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            string strExecution = "[report].[uspGetCustomerWithCallDetail]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@AreaId", detail.AreaId);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@UserId", userId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();

                    callDetail.AreaName = Convert.ToString(reader["AreaName"]);
                    callDetail.GroupCompanyName = Convert.ToString(reader["GroupName"]);
                    callDetail.Address = Convert.ToString(reader["Address"]);
                    callDetail.Contact = Convert.ToString(reader["Contact"]);
                    callDetail.Profile = Convert.ToString(reader["CustomerProfile"]);
                    callDetail.CallDate = Convert.ToDateTime(reader["CallDate"]);
                    callDetail.CallType = Convert.ToString(reader["CallType"]);

                    if (reader["NextCallOn"] != DBNull.Value)
                        callDetail.NextCallDate = Convert.ToDateTime(reader["NextCallOn"]);

                    callDetail.CallDetails = Convert.ToString(reader["Remarks"]);
                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }

        public static List<ICallDetail> GetMisReportData(DateTime fromDate, DateTime toDate, ICallDetail detail, int userId)
        {
            string strExecution = "[report].[uspGetMisReportData]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddDateTimeParam("@FromDate", fromDate);
                oDq.AddDateTimeParam("@ToDate", toDate);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddIntegerParam("@LineId", detail.ProspectId);
                oDq.AddIntegerParam("@UserId", userId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();
                    callDetail.LocationName = Convert.ToString(reader["LocName"]);
                    callDetail.ProspectFor = Convert.ToString(reader["ProspectName"]);
                    callDetail.GroupCompanyName = Convert.ToString(reader["GroupName"]);
                    callDetail.Destination = Convert.ToString(reader["Destination"]);
                    callDetail.SalesPersonName = Convert.ToString(reader["SalesPerson"]);
                    callDetail.TEU = Convert.ToInt32(reader["TEU"]);
                    callDetail.FEU = Convert.ToInt32(reader["FEU"]);
                    callDetail.Total = Convert.ToInt32(reader["Total"]);

                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }

        public static List<ICallDetail> GetYearlyMisReportData(int year, ICallDetail detail, char reportType, int userId)
        {
            string strExecution = "[report].[uspGetYearlyMisReportData]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Year", year);
                oDq.AddIntegerParam("@LocId", detail.LocationId);
                oDq.AddCharParam("@ReportType", 1, reportType);
                oDq.AddIntegerParam("@UserId", userId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();
                    callDetail.LocationId = Convert.ToInt32(reader["LocId"]);
                    callDetail.LocationName = Convert.ToString(reader["LocName"]);
                    callDetail.ProspectFor = Convert.ToString(reader["ProspectName"]);
                    callDetail.Month1 = Convert.ToInt32(reader["Month1"]);
                    callDetail.Month2 = Convert.ToInt32(reader["Month2"]);
                    callDetail.Month3 = Convert.ToInt32(reader["Month3"]);
                    callDetail.Month4 = Convert.ToInt32(reader["Month4"]);
                    callDetail.Month5 = Convert.ToInt32(reader["Month5"]);
                    callDetail.Month6 = Convert.ToInt32(reader["Month6"]);
                    callDetail.Month7 = Convert.ToInt32(reader["Month7"]);
                    callDetail.Month8 = Convert.ToInt32(reader["Month8"]);
                    callDetail.Month9 = Convert.ToInt32(reader["Month9"]);
                    callDetail.Month10 = Convert.ToInt32(reader["Month10"]);
                    callDetail.Month11 = Convert.ToInt32(reader["Month11"]);
                    callDetail.Month12 = Convert.ToInt32(reader["Month12"]);
                    callDetail.Total = Convert.ToInt32(reader["Total"]);

                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }
    }
}
