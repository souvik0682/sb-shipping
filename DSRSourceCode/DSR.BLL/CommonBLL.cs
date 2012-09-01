using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.DAL;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities.ResourceManager;
using DSR.Utilities;
using DSR.Utilities.Cryptography;
using System.Net.Mail;

namespace DSR.BLL
{
    public class CommonBLL
    {
        #region Common

        #region Email

        public static bool SendMail(string from, string mailTo, string cc, string subject, string body, string mailServerIP)
        {
            bool sent = true;

            try
            {
                if (mailTo != "")
                {
                    MailMessage MyMail = new MailMessage();
                    MyMail.To.Add(new MailAddress(mailTo));
                    MyMail.Priority = MailPriority.High;
                    MyMail.From = new MailAddress(from, "DSR Help Desk");

                    if (cc != "")
                    {
                        MailAddress ccAddr = new MailAddress(cc);
                        MyMail.CC.Add(ccAddr);
                    }

                    MyMail.Subject = subject;
                    MyMail.Body = GetMessageBody(body);
                    MyMail.BodyEncoding = System.Text.Encoding.ASCII;
                    MyMail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient(mailServerIP);
                    client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    client.Send(MyMail);
                }
                else { sent = false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sent;
        }

        public static bool SendMail(string from, string mailTo, string cc, string subject, string body, string mailServerIP, string mailUserAccount, string mailUserPwd)
        {
            bool sent = true;

            try
            {
                if (mailTo != "")
                {
                    MailMessage MyMail = new MailMessage();
                    MyMail.To.Add(new MailAddress(mailTo));
                    MyMail.Priority = MailPriority.High;
                    MyMail.From = new MailAddress(from, "BEN LINE AGENCIES Help Desk");

                    if (cc != "")
                    {
                        MailAddress ccAddr = new MailAddress(cc);
                        MyMail.CC.Add(ccAddr);
                    }

                    MyMail.Subject = subject;
                    MyMail.Body = GetMessageBody(body);
                    MyMail.BodyEncoding = System.Text.Encoding.ASCII;
                    MyMail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient(mailServerIP, 25);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential(mailUserAccount, mailUserPwd);
                    client.Credentials = credential;
                    client.Send(MyMail);
                }
                else { sent = false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sent;
        }

        public static string GetMessageBody(string strBodyContent)
        {
            try
            {
                StringBuilder sbMsgBody = new StringBuilder();
                sbMsgBody.Append("<font face='Verdana, Arial, Helvetica, sans-serif' size='10' color='#8B4B0D'>Daily Sales Call</font>");
                sbMsgBody.Append("<br>");
                sbMsgBody.Append("<br><br><br>");
                sbMsgBody.Append("<font face=verdana size=2>" + strBodyContent + "</font>");

                return sbMsgBody.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

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

        #region Role

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

        public List<ILocation> GetLocationByUser(int userId)
        {
            return CommonDAL.GetLocationByUser(userId);
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

        public List<IArea> GetAreaByLocationAndPinCode(int locId, string pinCode)
        {
            return CommonDAL.GetAreaByLocationAndPinCode(locId, pinCode);
        }

        #endregion

        #region Group Company

        private void SetDefaultSearchCriteriaForGroupCompany(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "GroupName";
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
            searchCriteria.SortExpression = "CustName";
            searchCriteria.SortDirection = "ASC";
        }

        public List<ICustomer> GetAllCustomer(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetCustomerList('N', searchCriteria);
        }

        public List<ICustomer> GetActiveCustomer(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetCustomerList('Y', searchCriteria);
        }

        public List<ICustomer> GetAllCustomer()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);
            return CommonDAL.GetCustomerList('N', searchCriteria);
        }

        public List<ICustomer> GetActiveCustomer()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);

            return CommonDAL.GetCustomerList('N', searchCriteria);
        }

        public List<ICustomer> GetCustomerByUser(int userId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForCustomer(searchCriteria);
            searchCriteria.UserId = userId;
            return CommonDAL.GetCustomerList('N', searchCriteria);
        }

        //public List<ICustomer> GetAllCustomer()
        //{
        //    SearchCriteria searchCriteria = new SearchCriteria();
        //    SetDefaultSearchCriteriaForCustomer(searchCriteria);
        //    return CommonDAL.GetCustomer('N', searchCriteria);
        //}

        //public List<ICustomer> GetActiveCustomer()
        //{
        //    SearchCriteria searchCriteria = new SearchCriteria();
        //    SetDefaultSearchCriteriaForCustomer(searchCriteria);
        //    return CommonDAL.GetCustomer('Y', searchCriteria);
        //}

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

        #region Assign Customer

        public List<ICustomerAssign> GetAssignedCustomer()
        {
            return CommonDAL.GetAssignedCustomer();
        }

        public ICustomerAssign GetAssignedCustomer(int custAssignID)
        {
            return CommonDAL.GetAssignedCustomer(custAssignID);
        }

        public int SaveAssignedCustomer(ICustomerAssign customer, int modifiedBy)
        {
            return CommonDAL.SaveAssignedCustomer(customer, modifiedBy);
        }

        public void DeleteAssignedCustomer(int id, int modifiedBy)
        {
            CommonDAL.DeleteAssignedCustomer(id, modifiedBy);
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

        #region Call Type

        public List<ICallType> GetAllCallType()
        {
            return CommonDAL.GetCallType('N');
        }

        public List<ICallType> GetActiveCallType()
        {
            return CommonDAL.GetCallType('Y');
        }

        public ICallType GetCallType(int callTypeId)
        {
            return CommonDAL.GetCallType(callTypeId, 'N');
        }

        #endregion

        #region Prospect

        public List<IProspect> GetAllProspect()
        {
            return CommonDAL.GetProspect('N');
        }

        public List<IProspect> GetActiveProspect()
        {
            return CommonDAL.GetProspect('Y');
        }

        public IProspect GetProspect(int prospectId)
        {
            return CommonDAL.GetProspect(prospectId, 'N');
        }

        #endregion

        #region Port

        public List<IPort> GetAllPort()
        {
            return CommonDAL.GetPort('N');
        }

        public List<IPort> GetActivePort()
        {
            return CommonDAL.GetPort('Y');
        }

        public IPort GetPort(int portId)
        {
            return CommonDAL.GetPort(portId, 'N');
        }

        #endregion

        #region Commitment

        //public List<ICommitment> GetCommitment()
        //{
        //    return CommonDAL.GetCommitment();
        //}

        //public ICommitment GetCommitment(int commitmentId)
        //{
        //    return CommonDAL.GetCommitment(commitmentId);
        //}

        //public void SaveCommitment(ICommitment commitment, int modifiedBy)
        //{
        //    CommonDAL.SaveCommitment(commitment, modifiedBy);
        //}

        #endregion

        #region Sales Call

        //private void SetDefaultSearchCriteriaForDSC(SearchCriteria searchCriteria)
        //{
        //    searchCriteria.SortExpression = "Location";
        //    searchCriteria.SortDirection = "ASC";
        //}

        public List<IDailySalesCall> GetDailySalesCallList(int userId)
        {
            return CommonDAL.GetDailySalesCallList(userId);
        }

        public IDailySalesCall GetDailySalesCall(int callId)
        {
            return CommonDAL.GetDailySalesCall(callId);
        }

        public void DeleteDailySalesCall(int callId, int modifiedBy)
        {
            CommonDAL.DeleteDailySalesCall(callId, modifiedBy);
        }

        #endregion

        #region Import Data

        public List<IShipSoft> GetShipSoftData(int custId, bool isTagged)
        {
            return CommonDAL.GetShipSoftData(custId, isTagged);
        }

        public void SaveShipSoft(List<ShipSoftEntity> lstShipSoft, int modifiedBy, out int rowsAffected, out int dupCount)
        {
            string xmlDoc = GeneralFunctions.Serialize(lstShipSoft);
            CommonDAL.SaveShipSoft(xmlDoc, modifiedBy, out rowsAffected, out dupCount);
        }

        public void TagCustomer(List<ShipSoftEntity> lstShipSoft, int custId, int modifiedBy)
        {
            string xmlDoc = GeneralFunctions.Serialize(lstShipSoft);
            CommonDAL.TagCustomer(xmlDoc, custId, modifiedBy);
        }

        #endregion

        #region User

        //public List<IUser> GetSalesExecutive()
        //{
        //    return CommonDAL.GetSalesExecutive();
        //}

        public List<IUser> GetSalesExecutive(int userId)
        {
            return CommonDAL.GetSalesExecutive(userId);
        }

        public List<IUser> GetSalesExecutiveNew(int userId)
        {
            return CommonDAL.GetSalesExecutiveNew(userId);
        }

        #endregion
    }
}
