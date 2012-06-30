using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface ICommon
    {
        int CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        int ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
