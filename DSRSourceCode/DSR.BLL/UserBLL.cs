using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.DAL;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities;
using System.Web;
using DSR.Utilities.ResourceManager;

namespace DSR.BLL
{
    public class UserBLL
    {
        public static int GetLoggedInUserId()
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

        private void SetDefaultSearchCriteriaForUser(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "UserName";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IUser> GetAllUserList(SearchCriteria searchCriteria)
        {
            return UserDAL.GetUserList('N', searchCriteria);
        }

        public List<IUser> GetActiveUserList()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForUser(searchCriteria);
            return UserDAL.GetUserList('Y', searchCriteria);
        }

        public IUser GetUser(int userId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForUser(searchCriteria);
            return UserDAL.GetUser(userId, 'N', searchCriteria);
        }

        public string SaveUser(IUser user, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = UserDAL.SaveUser(user, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00015");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteUser(int userId, int modifiedBy)
        {
            UserDAL.DeleteUser(userId, modifiedBy);
        }
    }
}
