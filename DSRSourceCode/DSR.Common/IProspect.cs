using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IProspect : IBase<int>
    {
        char IsActive { get; set; }
    }
}
