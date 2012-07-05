using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSR.Common
{
    public interface IUser : IBase<int>, ICommon
    {
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        IRole UserRole { get; set; }
        ILocation UserLocation { get; set; }
        string EmailId { get; set; }
        char? SalesPersonType { get; set; }
        char IsActive { get; set; }
    }
}
