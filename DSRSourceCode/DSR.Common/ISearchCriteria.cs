using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface ISearchCriteria
    {
        string SortExpression { get; set; }
        string SortDirection { get; set; }
    }
}
