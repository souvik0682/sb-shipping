using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using DSR.Utilities;

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

        public string ExecutiveName
        {
            get;
            set;
        }

        public int UserId
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

        public PageName CurrentPage
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
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

        #region Public Methods

        public void Clear()
        {
            this.AreaName = string.Empty;
            this.CustomerName = string.Empty;
            this.ExecutiveName = string.Empty;
            this.FirstName = string.Empty;
            this.GroupName = string.Empty;
            this.LocAbbr = string.Empty;
            this.LocName = string.Empty;
            this.SortDirection = string.Empty;
            this.SortExpression = string.Empty;
            this.UserId = 0;
            this.UserName = string.Empty;
            this.CurrentPage = 0;
            this.PageIndex = 0;
            this.PageSize = 0;
        }

        #endregion
    }
}
