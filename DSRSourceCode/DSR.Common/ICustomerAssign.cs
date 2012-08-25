using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface ICustomerAssign : ICommon
    {
        int Id { get; set; }
        int ExistingUserId { get; set; }
        string ExistingUserName { get; set; }
        int NewUserId { get; set; }
        string NewUserName { get; set; }
        char AssignType { get; set; }
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}
