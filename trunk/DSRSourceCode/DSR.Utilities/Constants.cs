using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Utilities
{
    #region Enumerations

    /// <summary>
    /// Specifies the report format in which the report will be generated.
    /// </summary>
    /// <createdby>Amit Kumar Chandra</createdby>
    /// <createddate>30/06/2012</createddate>
    public enum ReportFormat
    {
        /// <summary>
        /// Report will be generated in PDF format.
        /// </summary>
        PDF = 1,
        /// <summary>
        /// Report will be generated in Ms-Excel format.
        /// </summary>
        Excel = 2,
        /// <summary>
        /// Report will be generated in XML format.
        /// </summary>
        XML = 3
    }

    #endregion

    public static class Constants
    {
        #region Constants

        public const string EMAIL_REGX_EXP = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" + @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" + @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

        #endregion

        #region Session Variables

        public const string DEFAULT_CULTURE = "en-US";
        public const string DATA_VALUE_FIELD = "ListItemValue";
        public const string DATA_TEXT_FIELD = "ListItemText";
        public const string DROPDOWNLIST_DEFAULT_VALUE = "0";
        public const string DROPDOWNLIST_DEFAULT_TEXT = "--Select--";
        public const string DROPDOWNLIST_ALL_TEXT = "All";
        public const string NOT_AVAILABLE_TEXT = "Not Available";
        public const string SORT_EXPRESSION = "SortExpression";
        public const string SORT_DIRECTION = "SortDirection";

        #endregion

        #region Session Variables

        public const string SESSION_SEARCH_CRITERIA = "SearchCriteria";
        public const string SESSION_USER_INFO = "UserInfo";
        #endregion
    }
}
