using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class ContactPersonEntity : IContactPerson
    {
        #region IContactPerson Members

        public string Name
        {
            get;
            set;
        }

        public string Designation
        {
            get;
            set;
        }

        public string Mobile
        {
            get;
            set;
        }

        public string EmailId
        { 
            get; 
            set; 
        }

        #endregion

        #region Constructors

        public ContactPersonEntity()
        {

        }

        public ContactPersonEntity(DataTableReader reader)
        {
            this.Name = Convert.ToString(reader["Name"]);
            this.Designation = Convert.ToString(reader["Designation"]);
            this.Mobile = Convert.ToString(reader["Mobile"]);
            this.EmailId = Convert.ToString(reader["EmailId"]);
        }

        #endregion
    }
}
