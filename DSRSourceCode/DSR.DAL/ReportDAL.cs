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

        public static List<ICallDetail> GetDailyCallData()
        {
            string strExecution = "[report].[uspGetDailyCall]";
            List<ICallDetail> lstCallDetail = new List<ICallDetail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                //oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICallDetail callDetail = new CallDetailEntity();
                    callDetail.ProspectFor = Convert.ToString(reader["ProspectName"]);
                    callDetail.CallDate = Convert.ToDateTime(reader["CallDate"]);
                    callDetail.GroupCompany = Convert.ToString(reader["GroupCompany"]);
                    callDetail.CallType = Convert.ToString(reader["CallType"]);

                    if (reader["NextCallOn"] != DBNull.Value)
                        callDetail.NextCallDate = Convert.ToDateTime(reader["NextCallOn"]);

                    callDetail.CallDetails = Convert.ToString(reader["Remarks"]);

                    if (reader["LocId"] != DBNull.Value)
                    {
                        callDetail.LocationId = Convert.ToInt32(reader["LocId"]);
                        callDetail.Location = Convert.ToString(reader["LocName"]);
                    }

                    if (reader["SalesPersionId"] != DBNull.Value)
                    {
                        callDetail.SalesPersionId = Convert.ToInt32(reader["SalesPersionId"]);
                        callDetail.SalesPerson = Convert.ToString(reader["SalesPerson"]);
                    }

                    lstCallDetail.Add(callDetail);
                }

                reader.Close();
            }

            return lstCallDetail;
        }

    }
}
