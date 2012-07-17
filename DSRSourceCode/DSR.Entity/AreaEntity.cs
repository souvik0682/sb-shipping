using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class AreaEntity : IArea
    {        
        #region IArea Members

        public string PinCode
        {
            get;
            set;
        }

        public ILocation Location
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

        public AreaEntity()
        {
            this.Location = new LocationEntity();
        }

        public AreaEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["Id"]);
            this.Name  = Convert.ToString(reader["Name"]);
            this.PinCode = Convert.ToString(reader["PinCode"]);
            this.Location = new LocationEntity();
            this.Location.Id = Convert.ToInt32(reader["LocId"]);
            this.Location.Name = Convert.ToString(reader["LocName"]);
            this.IsActive = Convert.ToChar(reader["Active"]);
        }       

        #endregion
    }
}
