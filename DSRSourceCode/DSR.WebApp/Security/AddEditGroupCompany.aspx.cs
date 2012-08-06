using System;
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

namespace DSR.WebApp.Security
{
    public partial class AddEditGroupCompany : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _groupId = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveGroup();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageGroupCompany.aspx','" + ResourceManager.GetStringWithoutName("ERR00046") + "')";
                txtAddress.Attributes.Add("onkeypress", "javascript:return SetMaxLength(this, 200);");
                txtName.Attributes.Add("onkeypress", "ConvertToUpperCase(event);");
                txtCity.Attributes.Add("onkeypress", "ConvertToUpperCase();");
                txtPin.Attributes.Add("onkeypress", "ConvertToUpperCase();");

                revPhone.ValidationExpression = Constants.PHONE_REGX_EXP;
                revPhone.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00047");
                rfvName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00024");
            }

            if (_groupId == -1)
            {
                chkActive.Checked = true;
                chkActive.Enabled = false;
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _groupId);
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

            if (_groupId == 0)
                Response.Redirect("~/Security/ManageGroupCompany.aspx");
        }

        private void LoadData()
        {
            IGroupCompany groupCompany = new CommonBLL().GetGroupCompany(_groupId);

            if (!ReferenceEquals(groupCompany, null))
            {
                txtName.Text = groupCompany.Name;

                if (!ReferenceEquals(groupCompany.Address, null))
                {
                    txtAddress.Text = groupCompany.Address.Address;
                    txtCity.Text = groupCompany.Address.City;
                    txtPin.Text = groupCompany.Address.Pin;
                }

                txtPhone.Text = groupCompany.Phone;

                if (groupCompany.IsActive == 'Y')
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;

                if (_groupId == 1)
                    chkActive.Enabled = false;
            }
        }

        private void SaveGroup()
        {
            CommonBLL commonBll = new CommonBLL();
            IGroupCompany groupCompany = new GroupCompanyEntity();
            string message = string.Empty;
            BuildGroupCompanyEntity(groupCompany);
            message = commonBll.SaveGroupCompany(groupCompany, _userId);

            if (message == string.Empty)
            {
                Response.Redirect("~/Security/ManageGroupCompany.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }
        }

        private void BuildGroupCompanyEntity(IGroupCompany groupCompany)
        {
            groupCompany.Id = _groupId;
            groupCompany.Name = txtName.Text.Trim().ToUpper();
            groupCompany.Address.Address = txtAddress.Text.Trim().ToUpper();
            groupCompany.Address.City = txtCity.Text.Trim().ToUpper();
            groupCompany.Address.Pin = txtPin.Text.Trim().ToUpper();
            groupCompany.Phone = txtPhone.Text.Trim().ToUpper();

            if (chkActive.Checked)
                groupCompany.IsActive = 'Y';
            else
                groupCompany.IsActive = 'N';
        }

        #endregion
    }
}