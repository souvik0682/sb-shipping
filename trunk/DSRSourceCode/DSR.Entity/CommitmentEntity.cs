using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.Common;
using System.Data;

namespace DSR.Entity
{
    public class CommitmentEntity : ICommitment
    {
        #region ICommitment Members

        public int CommitmentId
        {
            get;
            set;
        }

        public int CallId
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public int CustomerId
        {
            get;
            set;
        }

        public int WeekNo
        {
            get;
            set;
        }

        public int PortId
        {
            get;
            set;
        }

        public int TEU
        {
            get;
            set;
        }

        public int FEU
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public CommitmentEntity()
        {

        }

        public CommitmentEntity(DataTableReader reader)
        {
            this.CommitmentId = Convert.ToInt32(reader["CommitmentId"]);
            this.CallId = Convert.ToInt32(reader["CallId"]);
            this.UserId = Convert.ToInt32(reader["UserId"]);
            this.CustomerId = Convert.ToInt32(reader["CustomerId"]);
            this.WeekNo = Convert.ToInt32(reader["WeekNo"]);
            this.PortId = Convert.ToInt32(reader["PortId"]);
            this.TEU = Convert.ToInt32(reader["TEU"]);
            this.FEU = Convert.ToInt32(reader["FEU"]);
        }

        #endregion
    }
}
