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
    public sealed class DailySalesCallDAL
    {
        private DailySalesCallDAL()
        {
        }

        public static int ValidateDestination(string portCode)
        {
            int portId = -1;
            string strExecution = "[common].[uspGetPortByPortCode]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@PortCode", 6, portCode);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    if (reader["PortId"] != DBNull.Value)
                        portId = Convert.ToInt32(reader["PortId"]);
                }

                reader.Close();
            }
            return portId;
        }

        public static int SaveDailySalesCall(IDailySalesCall dailySalesCall)
        {
            string strExecution = "[common].[uspSaveDailySalesCall]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_CallID", dailySalesCall.CallId);
                oDq.AddIntegerParam("@fk_UserID", dailySalesCall.UserId);
                oDq.AddIntegerParam("@fk_CustID", dailySalesCall.CustomerId);
                oDq.AddIntegerParam("@fk_CallType", dailySalesCall.CallType);
                oDq.AddIntegerParam("@fk_ProspectID", dailySalesCall.ProspectId);

                oDq.AddDateTimeParam("@CallDate", dailySalesCall.CallDate);
                oDq.AddDateTimeParam("@NextCallOn", dailySalesCall.NextCallDate);

                oDq.AddVarcharParam("@Remarks", 200, dailySalesCall.Remarks);

                oDq.AddIntegerParam("@fk_UserAdded", dailySalesCall.CreatedBy);
                oDq.AddIntegerParam("@fk_UserLastEdited", dailySalesCall.ModifiedBy);

                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }

            return result;
        }

        public static int SaveCommitment(ICommitment commitment)
        {
            string strExecution = "[common].[uspSaveCommitmentDetails]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@fk_CallID", commitment.CallId);
                oDq.AddIntegerParam("@fk_CustID", commitment.CustomerId);
                oDq.AddIntegerParam("@WeekNo", commitment.WeekNo);
                oDq.AddIntegerParam("@fk_PortID", commitment.PortId);
                oDq.AddIntegerParam("@TEU", commitment.TEU);
                oDq.AddIntegerParam("@FEU", commitment.FEU);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }
            return result;
        }

        public static IDailySalesCall GetDailySalesCallEntry(int callId)
        {
            string strExecution = "[common].[uspGetDailySalesCall]";
            IDailySalesCall salesCall = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CallId", callId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    salesCall = new DailySalesCallEntity(reader);
                }

                reader.Close();
            }
            return salesCall;
        }

        public static List<ICommitment> GetCommitments(int callId)
        {
            string strExecution = "[common].[uspGetCommitments]";
            List<ICommitment> lstCommitment = new List<ICommitment>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CallId", callId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICommitment loc = new CommitmentEntity(reader);
                    lstCommitment.Add(loc);
                }

                reader.Close();
            }

            return lstCommitment;
        }

        public static IDailySalesCall GetSalesExecutiveByCallId(int callId)
        {
            string strExecution = "[common].[uspGetSalesExecutiveByCallId]";
            IDailySalesCall salesCall = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CallId", callId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    salesCall = new DailySalesCallEntity();
                    salesCall.UserName = Convert.ToString(reader["UserName"]);
                }

                reader.Close();
            }
            return salesCall;
        }
    }    
}
