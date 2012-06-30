using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IProspectFor : IBase<int>
    {
        char IsActive { get; set; }
    }
}
