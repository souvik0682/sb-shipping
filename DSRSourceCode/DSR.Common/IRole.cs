using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IRole : IBase<int>
    {
        char? SalesRole { get; set; }
    }
}
