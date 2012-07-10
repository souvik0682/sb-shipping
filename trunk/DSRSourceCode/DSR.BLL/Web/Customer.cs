using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;

namespace DSR.BLL.Web
{
    public class Customer : ICustomer
    {

        #region ICustomer Members

        public IGroupCompany Group
        {
            get;
            set;
        }

        public IArea Area
        {
            get;
            set;
        }

        public ICustomerType CustType
        {
            get;
            set;
        }

        public char CorporateOrLocal
        {
            get;
            set;
        }

        public IAddress Address
        {
            get;
            set;
        }

        public string Phone1
        {
            get;
            set;
        }

        public string Phone2
        {
            get;
            set;
        }

        public IContactPerson ContactPerson1
        {
            get;
            set;
        }

        public IContactPerson ContactPerson2
        {
            get;
            set;
        }

        public string CustomerProfile
        {
            get;
            set;
        }

        public string PAN
        {
            get;
            set;
        }

        public string TAN
        {
            get;
            set;
        }

        public string BIN
        {
            get;
            set;
        }

        public string IEC
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public char IsActive
        {
            get;
            set;
        }

        #endregion

        #region IBase<int> Members

        public int Id
        {
            get;
            set;
        }

        public string Name
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
