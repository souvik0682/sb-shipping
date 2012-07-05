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

        private void CheckUserAccess()
        {
            if (_uId == 0)
                Response.Redirect("~/Security/ManageUser.aspx");
        }

        private void PopulateRole()
        {
            CommonBLL commonBll = new CommonBLL();
            List<IRole> lstRole = commonBll.GetRole();
            GeneralFunctions.PopulateDropDownList(ddlRole, lstRole, "Id", "Name", true);
        }

        private void PopulateLocation()
        {
            CommonBLL commonBll = new CommonBLL();
            List<ILocation> lstLoc = commonBll.GetActiveLocationList();
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

                if (user.UserRole.SalesRole == 'Y')
                    ddlSalesPersonType.SelectedValue = Convert.ToString(user.SalesPersonType);

                if (user.IsActive == 'Y')
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;
            }
        }

        private void SaveUser()
        {
            UserBLL userBll = new UserBLL();
            IUser user = new UserEntity();
            string message = string.Empty;
            BuildUserEntity(user);
            message = userBll.SaveUser(user, _userId);

            if (message == string.Empty)
            {
                Response.Redirect("~/Security/ManageUser.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }
        }

        private void BuildUserEntity(IUser user)
        {
            user.Id = _uId;
            user.Name = txtUserName.Text;
            user.FirstName = txtFName.Text;
            user.LastName = txtLName.Text;
            user.EmailId = txtEmail.Text;
            user.UserRole.Id = Convert.ToInt32(ddlRole.SelectedValue);
            user.UserLocation.Id = Convert.ToInt32(ddlLoc.SelectedValue);
            user.SalesPersonType = Convert.ToChar(ddlSalesPersonType.SelectedValue);

            if (chkActive.Checked)
                user.IsActive = 'Y';
            else
                user.IsActive = 'N';
        }

        #endregion
    }
}