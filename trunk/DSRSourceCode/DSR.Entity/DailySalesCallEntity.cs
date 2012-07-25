using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class DailySalesCallEntity : IDailySalesCall
    {
        #region IDailySalesCall Members

        public int CallId
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public int CustomerId
        {
            get;
            set;
        }

        public int CallType
        {
            get;
            set;
        }

        public int ProspectId
        {
            get;
            set;
        }

        public DateTime CallDate
        {
            get;
            set;
        }

        public DateTime? NextCallDate
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string CustomerName
        {
            get;
            set;
        }

        public string CallTypes
        {
            get;
            set;
        }

        public string Prospect
        {
            get;
            set;
        }

        #endregion

        #region ICommon Members

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public DailySalesCallEntity()
        {

        }

        public DailySalesCallEntity(DataTableReader reader)
        {
            this.CallId = Convert.ToInt32(reader["CallId"]);
            this.UserId = Convert.ToInt32(reader["UserId"]);
            this.CustomerId = Convert.ToInt32(reader["CustomerId"]);
            this.CallType = Convert.ToInt32(reader["CallType"]);
            this.ProspectId = Convert.ToInt32(reader["ProspectId"]);
            this.CallDate = Convert.ToDateTime(reader["CallDate"]);

            if (reader["NextCallDate"] != DBNull.Value)
                this.NextCallDate = Convert.ToDateTime(reader["NextCallDate"]);

            this.Remarks = Convert.ToString(reader["Remarks"]);

            if (HasColumn(reader, "UserName") && reader["UserName"] != DBNull.Value)
                this.UserName = Convert.ToString(reader["UserName"]);

            if (HasColumn(reader, "CustomerName") && reader["CustomerName"] != DBNull.Value)
                this.CustomerName = Convert.ToString(reader["CustomerName"]);

            if (HasColumn(reader, "CallTypes") && reader["CallTypes"] != DBNull.Value)
                this.CallTypes = Convert.ToString(reader["CallTypes"]);

            if (HasColumn(reader, "ProspectName") && reader["ProspectName"] != DBNull.Value)
                this.Prospect = Convert.ToString(reader["ProspectName"]);
        }

        #endregion

        private bool HasColumn(DataTableReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
