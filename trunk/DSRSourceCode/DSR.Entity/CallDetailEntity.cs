using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;

namespace DSR.Entity
{
    public class CallDetailEntity : ICallDetail
    {
        #region ICallDetail Members

        public string Location
        {
            get;
            set;
        }

        public string ProspectFor
        {
            get;
            set;
        }

        public DateTime CallDate
        {
            get;
            set;
        }

        public string GroupCompany
        {
            get;
            set;
        }

        public string CallType
        {
            get;
            set;
        }

        public DateTime? NextCallDate
        {
            get;
            set;
        }

        public string CallDetails
        {
            get;
            set;
        }

        public string SalesPerson
        {
            get;
            set;
        }

        public string SalesPersionId
        {
            get;
            set;
        }

        public decimal Area
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Contact
        {
            get;
            set;
        }

        public string Profile
        {
            get;
            set;
        }

        public string Line
        {
            get;
            set;
        }

        public string Destination
        {
            get;
            set;
        }

        #endregion
    }
}
