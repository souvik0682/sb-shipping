using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IDailySalesCall : ICommon
    {
        int CallId { get; set; }
        IUser User { get; set; }
        ICustomer Customer { get; set; }
        ICallType CallType { get; set; }
        IProspectFor Prospect { get; set; }
        DateTime CallDate { get; set; }
        DateTime NextCallDate { get; set; }
        string Remarks { get; set; }
    }
}
