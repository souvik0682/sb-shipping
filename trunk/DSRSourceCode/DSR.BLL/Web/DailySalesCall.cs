using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;

namespace DSR.BLL.Web
{
    public class DailySalesCall : IDailySalesCall
    {

        #region IDailySalesCall Members

        public int CallId
        {
            get;
            set;
        }

        public IUser User
        {
            get;
            set;
        }

        public ICustomer Customer
        {
            get;
            set;
        }

        public ICallType CallType
        {
            get;
            set;
        }

        public IProspectFor Prospect
        {
            get;
            set;
        }

        public DateTime CallDate
        {
            get;
            set;
        }

        public DateTime NextCallDate
        {
            get;
            set;
        }

        public string Remarks
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
    }
}
