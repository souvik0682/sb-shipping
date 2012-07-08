using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.DAL;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities.ResourceManager;
using DSR.Utilities;

namespace DSR.BLL
{
    public class CommonBLL
    {
        #region Common

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The <see cref="System.Exception"/> object.</param>
        /// <param name="logFilePath">The log file path.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>08/07/2012</createddate>
        public static void HandleException(Exception ex, string logFilePath)
        {
            int userId = 0;
            string userDetail = string.Empty;
            string baseException = string.Empty;

            if (ex.GetType() != typeof(System.Threading.ThreadAbortException))
            {
                if (System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO] != null)
                {
                    IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                    if (!ReferenceEquals(user, null))
                    {
                        userId = user.Id;
                        //userDetail = user.Id.ToString() + ", " + user.FirstName + " " + user.LastName;
                    }
                }

                if (ex.GetBaseException() != null)
                {
                    baseException = ex.GetBaseException().ToString();
                }
                else
                {
                    baseException = ex.StackTrace;
                }

                try
                {
                    CommonDAL.SaveErrorLog(userId, ex.Message, baseException);
                }
                catch
                {
                    //try
                    //{
                    //    string message = DateTime.UtcNow.ToShortDateString().ToString() + " "
                    //            + DateTime.UtcNow.ToLongTimeString().ToString() + " ==> " + "User Id: " + userDetail + "\r\n"
                    //            + ex.GetBaseException().ToString();

                    //    GeneralFunctions.WriteErrorLog(logFilePath + LogFileName, message);
                    //}
                    //catch
                    //{
                    //    // Consume the exception.
                    //}
                }
            }
        }

        #endregion

        #region Location

        public List<IRole> GetRole()
        {
            return CommonDAL.GetRole();
        }

        public IRole GetRole(int roleId)
        {
            return CommonDAL.GetRole(roleId);
        }

        #endregion

        #region Location

        private void SetDefaultSearchCriteriaForLocation(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<ILocation> GetAllLocation(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetLocation('N', searchCriteria);
        }

        public List<ILocation> GetActiveLocation()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetLocation('Y', searchCriteria);
        }

        public ILocation GetLocation(int locId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetLocation(locId, 'N', searchCriteria);
        }

        public string SaveLocation(ILocation loc, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CommonDAL.SaveLocation(loc, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00011");
                    break;
                case 2:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00012");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteLocation(int locId, int modifiedBy)
        {
            CommonDAL.DeleteLocation(locId, modifiedBy);
        }

        #endregion

        #region Area

        private void SetDefaultSearchCriteriaForArea(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IArea> GetAllArea(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetArea('N', searchCriteria);
        }

        public List<IArea> GetActiveArea()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForArea(searchCriteria);
            return CommonDAL.GetArea('Y', searchCriteria);
        }

        public IArea GetArea(int areaId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForArea(searchCriteria);
            return CommonDAL.GetArea(areaId, 'N', searchCriteria);
        }

        public string SaveArea(IArea area, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CommonDAL.SaveArea(area, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00013");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteArea(int areaId, int modifiedBy)
        {
            CommonDAL.DeleteArea(areaId, modifiedBy);
        }

        public List<IArea> GetAreaByLocation(int locId)
        {
            return CommonDAL.GetAreaByLocation(locId);
        }

        #endregion

        #region Group Company

        private void SetDefaultSearchCriteriaForGroupCompany(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IGroupCompany> GetAllGroupCompany(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetGroupCompany('N', searchCriteria);
        }

        public List<IGroupCompany> GetActiveGroupCompany()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForGroupCompany(searchCriteria);
            return CommonDAL.GetGroupCompany('Y', searchCriteria);
        }

        public IGroupCompany GetGroupCompany(int groupCompanyId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetGroupCompany(groupCompanyId, 'N', searchCriteria);
        }

        public string SaveGroupCompany(IGroupCompany groupCompany, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CommonDAL.SaveGroupCompany(groupCompany, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00014");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteGroupCompany(int groupCompanyId, int modifiedBy)
        {
            CommonDAL.DeleteGroupCompany(groupCompanyId, modifiedBy);
        }

        #endregion

        #region Customer

        private void SetDefaultSearchCriteriaForCustomer(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<ICustomer> GetAllCustomer(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetCustomer('N', searchCriteria);
        }

        public List<ICustomer> GetActiveCustomer()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);
            return CommonDAL.GetCustomer('Y', searchCriteria);
        }

        public ICustomer GetCustomer(int customer)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);
            return CommonDAL.GetCustomer(customer, 'N', searchCriteria);
        }

        public string SaveCustomer(ICustomer customer, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CommonDAL.SaveCustomer(customer, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00013");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteCustomer(int customer, int modifiedBy)
        {
            CommonDAL.DeleteCustomer(customer, modifiedBy);
        }

        #endregion

        #region Customer Type

        public List<ICustomerType> GetAllCustomerType()
        {
            return CommonDAL.GetCustomerType('N');
        }

        public List<ICustomerType> GetActiveCustomerType()
        {
            return CommonDAL.GetCustomerType('Y');
        }

        public ICustomerType GetCustomerType(int custTypeId)
        {
            return CommonDAL.GetCustomerType(custTypeId, 'N');
        }

        #endregion

        #region User

        public List<IUser> GetSalesExecutive()
        {
            return CommonDAL.GetSalesExecutive();
        }

        #endregion
    }
}
