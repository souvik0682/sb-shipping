using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.DAL;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities;
using System.Web;

namespace DSR.BLL
{
    public class UserBLL
    {
        public static int GetUserId()
        {
            int userId = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null))
                {
                    userId = user.Id;
                }
            }

            return userId;
        }

        public List<IUser> GetAllUser(ISearchCriteria searchCriteria)
        {
            return UserDAL.GetAllUser(searchCriteria);
        }
    }
}
