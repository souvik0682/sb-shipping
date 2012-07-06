using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface ICustomer : IBase<int>, ICommon
    {
        IGroupCompany Group { get; set; }
        ILocation Location { get; set; }
        IArea Area { get; set; }
        ICustomerType CustType { get; set; }
        char CorporateOrLocal { get; set; }
        IAddress Address { get; set; }
        string Phone1 { get; set; }
        string Phone2 { get; set; }
        IContactPerson ContactPerson1 { get; set; }
        IContactPerson ContactPerson2 { get; set; }
        string CustomerProfile { get; set; }
        string PAN { get; set; }
        string TAN { get; set; }
        string BIN { get; set; }
        string IEC { get; set; }
        int UserId { get; set; }
        char IsActive { get; set; }
    }
}
