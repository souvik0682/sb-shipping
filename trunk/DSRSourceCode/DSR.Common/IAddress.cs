using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IAddress
    {
        string Address { get; set; }
        string City { get; set; }
        string Pin { get; set; }
    }
}
