using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class PortEntity : IPort
    {
        #region IPort Members

        public string Code
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

        #region Constructors

        public PortEntity()
        {

        }

        public PortEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["PortId"]);
            this.Name  = Convert.ToString(reader["PortName"]);
            this.Code = Convert.ToString(reader["PortCode"]);
            this.IsActive = Convert.ToChar(reader["Active"]);
        }       

        #endregion
    }
}
