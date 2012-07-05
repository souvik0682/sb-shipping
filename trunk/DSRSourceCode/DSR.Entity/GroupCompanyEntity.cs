using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class GroupCompanyEntity : IGroupCompany
    {
        #region IGroupCompany Members

        public IAddress Address
        {
            get;
            set;
        }

        public string Phone
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

        public GroupCompanyEntity()
        {
            this.Address = new AddressEntity();
        }

        public GroupCompanyEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["Id"]);
            this.Name  = Convert.ToString(reader["Name"]);
            this.Address = new AddressEntity(reader);
            this.Phone = Convert.ToString(reader["Phone"]);
            this.IsActive = Convert.ToChar(reader["Active"]);
        }

         #endregion
    }
}
