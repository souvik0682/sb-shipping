using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IPort : IBase<int>
    {
        string Code { get; set; }
        char IsActive { get; set; }
    }
}
