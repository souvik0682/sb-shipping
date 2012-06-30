﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;

namespace DSR.Entity
{
    public class RoleEntity : IRole
    {
        #region IRole Members

        public char SalesRole
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
    }
}
