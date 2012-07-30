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

        string Line
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
    }
}
