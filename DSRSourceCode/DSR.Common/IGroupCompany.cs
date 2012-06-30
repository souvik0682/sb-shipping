using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IGroupCompany : IBase<int>, ICommon
    {
        IAddress Address { get; set; }
        string Phone { get; set; }
        char IsActive { get; set; }
    }
}
