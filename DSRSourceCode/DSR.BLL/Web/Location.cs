using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;

namespace DSR.BLL.Web
{
    public class Location : ILocation
    {        
        #region ILocation Members

        public IAddress Address
        {
            get;
            set;
        }

        public string Abbreviation
        {
            get;
            set;
        }

        public List<string> Phone
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
