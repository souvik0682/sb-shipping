using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class ProspectEntity : IProspect
    {
        #region IProspect Members

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

        public ProspectEntity()
        {

        }

        public ProspectEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["ProspectId"]);
            this.Name = Convert.ToString(reader["ProspectName"]);
            this.IsActive = Convert.ToChar(reader["Active"]);
        }

        #endregion
    }
}
