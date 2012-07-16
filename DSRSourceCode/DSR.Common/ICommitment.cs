using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface ICommitment
    {
        int CommitmentId { get; set; }
        int CallId { get; set; }
        int UserId { get; set; }
        int CustomerId { get; set; }
        int WeekNo { get; set; }
        int PortId { get; set; }
        int TEU { get; set; }
        int FEU { get; set; }
    }
}
