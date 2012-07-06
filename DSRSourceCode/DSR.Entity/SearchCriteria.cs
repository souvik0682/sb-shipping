using System;
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

        public string LocAbbr
        {
            get;
            set;
        }

        public string LocName
        {
            get;
            set;
        }

        public string AreaName
        {
            get;
            set;
        }

        public string GroupName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string CustomerName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

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
