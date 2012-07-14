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
                txtAddress.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 200)";
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
            groupCompany.Name = txtName.Text;
            groupCompany.Address.Address = txtAddress.Text;
            groupCompany.Address.City = txtCity.Text;
            groupCompany.Address.Pin = txtPin.Text;
            groupCompany.Phone = txtPhone.Text;

            if (chkActive.Checked)
                groupCompany.IsActive = 'Y';
            else
                groupCompany.IsActive = 'N';
        }

        #endregion
    }
}