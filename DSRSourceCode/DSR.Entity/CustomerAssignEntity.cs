using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DSR.Common;

namespace DSR.Entity
{
    public class CustomerAssignEntity : ICustomerAssign
    {
        #region IAssignCustomer Members

        public int Id
        {
            get;
            set;
        }

        public int ExistingUserId
        {
            get;
            set;
        }

        public string ExistingUserName
        {
            get;
            set;
        }

        public int NewUserId
        {
            get;
            set;
        }

        public string NewUserName
        {
            get;
            set;
        }

        public char AssignType
        {
            get;
            set;
        }

        public DateTime StartDate
        {
            get;
            set;
        }

        public DateTime? EndDate
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

        public CustomerAssignEntity()
        {

        }

        public CustomerAssignEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["CustAssignID"]);
            this.ExistingUserId = Convert.ToInt32(reader["ExistingUserId"]);
            this.ExistingUserName = Convert.ToString(reader["ExistingUserName"]);
            this.NewUserId = Convert.ToInt32(reader["NewUserId"]);
            this.NewUserName = Convert.ToString(reader["NewUserName"]);
            this.AssignType = Convert.ToChar(reader["AssignType"]);
            this.StartDate = Convert.ToDateTime(reader["StartDate"]);

            if (reader["EndDate"] != DBNull.Value)
                this.EndDate = Convert.ToDateTime(reader["EndDate"]);
        }

        #endregion
    }
}
