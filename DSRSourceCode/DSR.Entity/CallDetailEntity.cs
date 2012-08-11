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

        public int LocationId
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }

        public int ProspectId
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

        public int GroupCompanyId
        {
            get;
            set;
        }

        public string GroupCompanyName
        {
            get;
            set;
        }

        public int CallTypeId
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

        public int SalesPersionId
        {
            get;
            set;
        }

        public string SalesPersonName
        {
            get;
            set;
        }

        public int AreaId
        {
            get;
            set;
        }

        public string AreaName
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

        public string Destination
        {
            get;
            set;
        }

        public int TEU
        {
            get;
            set;
        }

        public int FEU
        {
            get;
            set;
        }

        public int TEUActual
        {
            get;
            set;
        }

        public int FEUActual
        {
            get;
            set;
        }

        public int Month1
        {
            get;
            set;
        }

        public int Month2
        {
            get;
            set;
        }

        public int Month3
        {
            get;
            set;
        }

        public int Month4
        {
            get;
            set;
        }

        public int Month5
        {
            get;
            set;
        }

        public int Month6
        {
            get;
            set;
        }

        public int Month7
        {
            get;
            set;
        }

        public int Month8
        {
            get;
            set;
        }

        public int Month9
        {
            get;
            set;
        }

        public int Month10
        {
            get;
            set;
        }

        public int Month11
        {
            get;
            set;
        }

        public int Month12
        {
            get;
            set;
        }

        public int Total
        {
            get;
            set;
        }

        #endregion
    }
}
