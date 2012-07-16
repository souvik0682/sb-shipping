using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class CallTypeEntity : ICallType
    {
        #region ICallType Members

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

        public CallTypeEntity()
        {

        }

        public CallTypeEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["CallTypeId"]);
            this.Name = Convert.ToString(reader["CallTypes"]);
            this.IsActive = Convert.ToChar(reader["Active"]);

        }

        #endregion
    }
}
