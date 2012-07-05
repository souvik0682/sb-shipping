using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class RoleEntity : IRole
    {
        #region IRole Members

        public char? SalesRole
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

        public RoleEntity()
        {

        }

        public RoleEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["RoleId"]);
            this.Name = Convert.ToString(reader["RoleName"]);

            if (reader["SalesRole"] != DBNull.Value)
                this.SalesRole = Convert.ToChar(reader["SalesRole"]);
        }

        #endregion
    }
}
