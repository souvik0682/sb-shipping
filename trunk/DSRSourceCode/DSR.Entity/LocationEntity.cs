using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class LocationEntity : ILocation
    {
        #region ILocation Members

        public IAddress LocAddress
        {
            get;
            set;
        }

        public string Abbreviation
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public int ManagerId
        {
            get;
            set;
        }

        public string ManagerName
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

        public LocationEntity()
        {
            this.LocAddress = new AddressEntity();
        }

        public LocationEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["Id"]);
            this.Name  = Convert.ToString(reader["Name"]);
            this.LocAddress = new AddressEntity(reader);
            this.Abbreviation = Convert.ToString(reader["LocAbbr"]);
            this.Phone = Convert.ToString(reader["LocPhone"]);

            if (reader["ManagerId"] != DBNull.Value)
            {
                this.ManagerId = Convert.ToInt32(reader["ManagerId"]);
                this.ManagerName = Convert.ToString(reader["ManagerName"]);
            }

            this.IsActive = Convert.ToChar(reader["Active"]);
        }       

        #endregion
    }
}
