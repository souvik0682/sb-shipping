using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IArea : IBase<int>, ICommon
    {
        ILocation Location { get; set; }
        char IsActive { get; set; }
    }
}
