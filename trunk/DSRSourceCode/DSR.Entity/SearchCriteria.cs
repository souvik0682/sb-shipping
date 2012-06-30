﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;

namespace DSR.Entity
{
    [Serializable]
    public class SearchCriteria : ISearchCriteria
    {
        #region Public Properties

        public string SortExpression
        {
            get;
            set;
        }

        public string SortDirection
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public SearchCriteria()
        {

        }

        #endregion
    }
}
