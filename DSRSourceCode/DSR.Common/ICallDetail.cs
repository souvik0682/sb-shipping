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

        string Location
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

        string GroupCompany
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

        string SalesPerson
        {
            get;
            set;
        }

        int SalesPersionId
        {
            get;
            set;
        }

        decimal Area
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
    }
}
