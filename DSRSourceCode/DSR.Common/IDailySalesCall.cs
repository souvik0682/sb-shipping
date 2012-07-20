using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IDailySalesCall : ICommon
    {
        int CallId { get; set; }
        int UserId { get; set; }
        int CustomerId { get; set; }
        int CallType { get; set; }
        int ProspectId { get; set; }
        DateTime CallDate { get; set; }
        DateTime? NextCallDate { get; set; }
        string Remarks { get; set; }
        string UserName { get; set; }
        string CustomerName { get; set; }
        string CallTypes { get; set; }
        string Prospect { get; set; }
    }
}
