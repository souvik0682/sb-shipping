using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IBase<T>
    {
        T Id { get; set; }
        string Name { get; set; }
    }
}
