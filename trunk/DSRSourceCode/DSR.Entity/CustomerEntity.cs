using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class CustomerEntity : ICustomer
    {

        #region ICustomer Members

        public IGroupCompany Group
        {
            get;
            set;
        }

        public ILocation Location
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

        public int? SalesExecutiveId
        {
            get;
            set;
        }

        public string SalesExecutiveName
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

        #region Constructors

        public CustomerEntity()
        {

        }

        public CustomerEntity(DataTableReader reader)
        {
            //Initialize properties
            this.Group = new GroupCompanyEntity();
            this.Location = new LocationEntity();
            this.Area = new AreaEntity();
            this.Address = new AddressEntity();
            this.ContactPerson1 = new ContactPersonEntity();
            this.ContactPerson2 = new ContactPersonEntity();

            this.Id = Convert.ToInt32(reader["CustID"]);
            this.Name = Convert.ToString(reader["CustName"]);
            
            this.Group.Id = Convert.ToInt32(reader["GroupID"]);
            this.Group.Name = Convert.ToString(reader["GroupName"]);
            this.Location.Id = Convert.ToInt32(reader["LocID"]);
            this.Location.Name = Convert.ToString(reader["LocName"]);
            this.Area.Id = Convert.ToInt32(reader["AreaID"]);
            this.Area.Name = Convert.ToString(reader["AreaName"]);
            this.CustType = new CustomerTypeEntity(reader);
            this.CorporateOrLocal = Convert.ToChar(reader["CorporateorLocal"]);
            this.Address.Address = Convert.ToString(reader["CustAddress"]);
            this.Address.City = Convert.ToString(reader["CustCity"]);
            this.Address.Pin = Convert.ToString(reader["CustPin"]);
            this.Phone1 = Convert.ToString(reader["CustPhone1"]);
            this.Phone2 = Convert.ToString(reader["CustPhone2"]);
            this.ContactPerson1.Name = Convert.ToString(reader["ContactPerson1"]);
            this.ContactPerson1.Designation = Convert.ToString(reader["ContactDesg1"]);
            this.ContactPerson1.Mobile = Convert.ToString(reader["ContactMob1"]);
            this.ContactPerson1.EmailId = Convert.ToString(reader["ContactEmailId1"]);
            this.ContactPerson2.Name = Convert.ToString(reader["ContactPerson2"]);
            this.ContactPerson2.Designation = Convert.ToString(reader["ContactDesg2"]);
            this.ContactPerson2.Mobile = Convert.ToString(reader["ContactMob2"]);
            this.ContactPerson2.EmailId = Convert.ToString(reader["ContactEmailId2"]);
            this.CustomerProfile = Convert.ToString(reader["CustomerProfile"]);
            this.PAN = Convert.ToString(reader["PANNo"]);
            this.TAN = Convert.ToString(reader["TANNo"]);
            this.BIN = Convert.ToString(reader["BINNo"]);
            this.IEC = Convert.ToString(reader["IECNo"]);

            if (reader["SalesExecutiveId"] != DBNull.Value)
            {
                this.SalesExecutiveId = Convert.ToInt32(reader["SalesExecutiveId"]);
                this.SalesExecutiveName = Convert.ToString(reader["SalesExecutiveName"]);
            }

            this.IsActive = Convert.ToChar(reader["Active"]);
        }

        #endregion

        #region Public Functions

        public void Initialize()
        {
            this.Group = new GroupCompanyEntity();
            this.Location = new LocationEntity();
            this.Area = new AreaEntity();
            this.CustType = new CustomerTypeEntity();
            this.Address = new AddressEntity();
            this.ContactPerson1 = new ContactPersonEntity();
            this.ContactPerson2 = new ContactPersonEntity();
        }

        #endregion
    }
}
