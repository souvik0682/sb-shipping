using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;

namespace DSR.BLL.Web
{
    public class User : IUser
    {

        #region IUser Members

        public string Password
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public IRole CustomerRole
        {
            get;
            set;
        }

        public ILocation CustomerLocation
        {
            get;
            set;
        }

        public string EmailId
        {
            get;
            set;
        }

        public char SalesPersonType
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
    }
}
