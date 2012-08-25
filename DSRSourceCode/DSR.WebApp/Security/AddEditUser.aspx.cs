﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.BLL;
using DSR.Utilities;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities.ResourceManager;
using System.Configuration;

namespace DSR.WebApp.Security
{
    public partial class AddEditUser : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _uId = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateRole();
                PopulateLocation();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveUser();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(ddlRole.SelectedValue))
            {
                case (int)UserRole.SalesExecutive:
                    ddlSalesPersonType.SelectedValue = "M";
                    ddlSalesPersonType.Enabled = true;
                    break;
                case (int)UserRole.Manager:
                    ddlSalesPersonType.Enabled = false;
                    ddlSalesPersonType.SelectedValue = "L";
                    break;
                default:
                    ddlSalesPersonType.Enabled = false;
                    ddlSalesPersonType.SelectedValue = "0";
                    break;
            }

            //if (IsSalesRole(Convert.ToInt32(ddlRole.SelectedValue)))
            //{
            //    ddlSalesPersonType.SelectedValue = "M";
            //    ddlSalesPersonType.Enabled = true;
            //}
            //else
            //{
            //    ddlSalesPersonType.Enabled = false;
            //    ddlSalesPersonType.SelectedValue = "0";
            //}
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _uId);
            }
        }

        private void SetAttributes()
        {
            spnName.Style["display"] = "none";
            spnFName.Style["display"] = "none";
            spnLName.Style["display"] = "none";
            spnEmail.Style["display"] = "none";
            spnRole.Style["display"] = "none";
            spnLoc.Style["display"] = "none";
            spnType.Style["display"] = "none";

            if (!IsPostBack)
            {
                //rfvUserName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00036");
                //rfvFName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00037");
                //rfvLName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00038");
                //rfvEmail.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00039");
                //rfvRole.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00040");
                //rfvLoc.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00025");

                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageUser.aspx','" + ResourceManager.GetStringWithoutName("ERR00046") + "')";
                revEmail.ValidationExpression = Constants.EMAIL_REGX_EXP;
                revEmail.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00023");

                spnName.InnerText = ResourceManager.GetStringWithoutName("ERR00036");
                spnFName.InnerText = ResourceManager.GetStringWithoutName("ERR00037");
                spnLName.InnerText = ResourceManager.GetStringWithoutName("ERR00038");
                spnEmail.InnerText = ResourceManager.GetStringWithoutName("ERR00039");
                spnRole.InnerText = ResourceManager.GetStringWithoutName("ERR00040");
                spnLoc.InnerText = ResourceManager.GetStringWithoutName("ERR00025");
                spnType.InnerText = ResourceManager.GetStringWithoutName("ERR00044");

                ddlSalesPersonType.Enabled = false;
            }

            if (_uId == -1)
            {
                chkActive.Checked = true;
                chkActive.Enabled = false;
            }

            if (_uId > 0)
            {
                txtUserName.Enabled = false;
            }
        }

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (_uId == 0)
                Response.Redirect("~/Security/ManageUser.aspx");
        }

        //private bool IsSalesRole(int roleId)
        //{
        //    bool isSalesRole = false;

        //    if (roleId == (int)UserRole.SalesExecutive)
        //    {
        //        isSalesRole = true;
        //    }
        //    else
        //    {
        //        isSalesRole = false;
        //    }

        //    //IRole role = new CommonBLL().GetRole(roleId);
        //    //bool isSalesRole = false;

        //    //if (!ReferenceEquals(role, null))
        //    //{
        //    //    if (role.SalesRole.HasValue && role.SalesRole.Value == 'Y')
        //    //    {
        //    //        isSalesRole = true;
        //    //    }
        //    //}

        //    return isSalesRole;
        //}

        private void PopulateRole()
        {
            CommonBLL commonBll = new CommonBLL();
            List<IRole> lstRole = commonBll.GetRole();
            GeneralFunctions.PopulateDropDownList(ddlRole, lstRole, "Id", "Name", true);
        }

        private void PopulateLocation()
        {
            CommonBLL commonBll = new CommonBLL();
            List<ILocation> lstLoc = commonBll.GetActiveLocation();
            GeneralFunctions.PopulateDropDownList(ddlLoc, lstLoc, "Id", "Name", true);
        }

        private void LoadData()
        {
            IUser user = new UserBLL().GetUser(_uId);

            if (!ReferenceEquals(user, null))
            {
                txtUserName.Text = user.Name;
                txtFName.Text = user.FirstName;
                txtLName.Text = user.LastName;
                txtEmail.Text = user.EmailId;
                ddlRole.SelectedValue = Convert.ToString(user.UserRole.Id);
                ddlLoc.SelectedValue = Convert.ToString(user.UserLocation.Id);

                if (user.UserRole.Id == (int)UserRole.SalesExecutive)
                {
                    ddlSalesPersonType.SelectedValue = Convert.ToString(user.SalesPersonType);
                    ddlSalesPersonType.Enabled = true;
                }
                else if (user.UserRole.Id == (int)UserRole.Manager)
                {
                    ddlSalesPersonType.SelectedValue = Convert.ToString(user.SalesPersonType);
                    ddlSalesPersonType.Enabled = false;
                }
                else
                {
                    ddlSalesPersonType.Enabled = false;
                }

                if (user.IsActive == 'Y')
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;

                if (_uId == 1)
                    chkActive.Enabled = false;
            }
        }

        private bool ValidateControls(IUser user)
        {
            bool isValid = true;

            if (user.Name == string.Empty)
            {
                isValid = false;
                spnName.Style["display"] = "";
            }

            if (user.FirstName == string.Empty)
            {
                isValid = false;
                spnFName.Style["display"] = "";
            }

            if (user.LastName == string.Empty)
            {
                isValid = false;
                spnLName.Style["display"] = "";
            }

            if (user.EmailId == string.Empty)
            {
                isValid = false;
                spnEmail.Style["display"] = "";
            }

            if (user.UserRole.Id == 0)
            {
                isValid = false;
                spnRole.Style["display"] = "";
                spnType.Style["display"] = "none";
            }
            else
            {
                IRole role = new CommonBLL().GetRole(user.UserRole.Id);

                if (!ReferenceEquals(role, null))
                {
                    if (role.SalesRole.HasValue && role.SalesRole.Value == 'Y')
                    {
                        if (user.SalesPersonType.Value == '0')
                        {
                            isValid = false;
                            spnType.Style["display"] = "";
                        }
                    }
                }
            }

            if (user.UserLocation.Id == 0)
            {
                isValid = false;
                spnLoc.Style["display"] = "";
            }

            return isValid;
        }

        private void SaveUser()
        {
            UserBLL userBll = new UserBLL();
            IUser user = new UserEntity();
            string message = string.Empty;
            BuildUserEntity(user);

            if (ValidateControls(user))
            {
                message = userBll.SaveUser(user, _userId);

                if (message == string.Empty)
                {
                    SendEmail(user);
                    Response.Redirect("~/Security/ManageUser.aspx");
                }
                else
                {
                    GeneralFunctions.RegisterAlertScript(this, message);
                }
            }
        }

        private void BuildUserEntity(IUser user)
        {
            user.Id = _uId;
            user.Name = txtUserName.Text.Trim().ToUpper();
            user.Password = UserBLL.GetDefaultPassword();
            user.FirstName = txtFName.Text.Trim().ToUpper();
            user.LastName = txtLName.Text.Trim().ToUpper();
            user.EmailId = txtEmail.Text.Trim().ToUpper();
            user.UserRole.Id = Convert.ToInt32(ddlRole.SelectedValue);
            user.UserLocation.Id = Convert.ToInt32(ddlLoc.SelectedValue);
            user.SalesPersonType = Convert.ToChar(ddlSalesPersonType.SelectedValue);

            if (chkActive.Checked)
                user.IsActive = 'Y';
            else
                user.IsActive = 'N';
        }

        private void SendEmail(IUser user)
        {
            string msgBody = "Dear " + user.UserFullName + "<br/>.Thank you for registering with BLA. <br/>Your login credentials:<br/>Username: " + user.Name + "<br/>Password:1234";

            try
            {
                CommonBLL.SendMail("", user.EmailId, string.Empty, "DSR Password Reset", "", Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]));
            }
            catch (Exception ex)
            {
                CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
            }
        }

        #endregion
    }
}