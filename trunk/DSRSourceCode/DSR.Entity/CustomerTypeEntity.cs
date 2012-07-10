using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class CustomerTypeEntity : ICustomerType
    {
        #region ICustomerType Members

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

        #region Constructors

        public CustomerTypeEntity()
        {

        }

        public CustomerTypeEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["CustTypeID"]);
            this.Name = Convert.ToString(reader["CustTypeName"]);
            this.IsActive = Convert.ToChar(reader["Active"]);
        }

        #endregion

    }
}
