using System;
using System.Collections.Generic;
using System.Text;

namespace DSR.Common
{
    public interface ICallDetail
    {
        int LocationId
        {
            get;
            set;
        }

        string LocationName
        {
            get;
            set;
        }

        int ProspectId
        {
            get;
            set;
        }

        string ProspectFor
        {
            get;
            set;
        }

        DateTime CallDate
        {
            get;
            set;
        }

        int GroupCompanyId
        {
            get;
            set;
        }

        string GroupCompanyName
        {
            get;
            set;
        }

        int CallTypeId
        {
            get;
            set;
        }

        string CallType
        {
            get;
            set;
        }

        DateTime? NextCallDate
        {
            get;
            set;
        }

        string CallDetails
        {
            get;
            set;
        }

        int SalesPersionId
        {
            get;
            set;
        }

        string SalesPersonName
        {
            get;
            set;
        }

        int AreaId
        {
            get;
            set;
        }

        string AreaName
        {
            get;
            set;
        }

        string Address
        {
            get;
            set;
        }

        string Contact
        {
            get;
            set;
        }

        string Profile
        {
            get;
            set;
        }

        string Destination
        {
            get;
            set;
        }

        int TEU
        {
            get;
            set;
        }

        int FEU
        {
            get;
            set;
        }

        int TEUActual
        {
            get;
            set;
        }

        int FEUActual
        {
            get;
            set;
        }

        int Month1
        {
            get;
            set;
        }

        int Month2
        {
            get;
            set;
        }

        int Month3
        {
            get;
            set;
        }

        int Month4
        {
            get;
            set;
        }

        int Month5
        {
            get;
            set;
        }

        int Month6
        {
            get;
            set;
        }

        int Month7
        {
            get;
            set;
        }

        int Month8
        {
            get;
            set;
        }

        int Month9
        {
            get;
            set;
        }

        int Month10
        {
            get;
            set;
        }

        int Month11
        {
            get;
            set;
        }

        int Month12
        {
            get;
            set;
        }

        int Total
        {
            get;
            set;
        }
    }
}
