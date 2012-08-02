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
    public partial class AddEditLocation : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateControls();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveLocation();

        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageLocation.aspx','" + ResourceManager.GetStringWithoutName("ERR00046") + "')";
                txtAddress.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 200)";
                revPhone.ValidationExpression = Constants.PHONE_REGX_EXP;
                revPhone.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00047");
                rfvName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00025");
                rfvAbbr.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00035");
            }

            if (_locId == -1)
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
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
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

            if (_locId == 0)
                Response.Redirect("~/Security/ManageLocation.aspx");
        }

        private void PopulateControls()
        {
            UserBLL userBll = new UserBLL();
            GeneralFunctions.PopulateDropDownList<IUser>(ddlManager, userBll.GetManagers(), "Id", "UserFullName", true);
        }

        private void LoadData()
        {
            ILocation location = new CommonBLL().GetLocation(_locId);

            if (!ReferenceEquals(location, null))
            {
                txtLocName.Text = location.Name;

                if (!ReferenceEquals(location.LocAddress, null))
                {
                    txtAddress.Text = location.LocAddress.Address;
                    txtCity.Text = location.LocAddress.City;
                    txtPin.Text = location.LocAddress.Pin;
                }

                txtAbbr.Text = location.Abbreviation;
                txtPhone.Text = location.Phone;

                ddlManager.SelectedValue = Convert.ToString(location.ManagerId);

                if (location.IsActive == 'Y')
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;
            }
        }

        private void SaveLocation()
        {
            CommonBLL commonBll = new CommonBLL();
            ILocation loc = new LocationEntity();
            string message = string.Empty;
            BuildLocationEntity(loc);
            message = commonBll.SaveLocation(loc, _userId);

            if (message == string.Empty)
            {
                Response.Redirect("~/Security/ManageLocation.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }
        }

        private void BuildLocationEntity(ILocation loc)
        {
            loc.Id = _locId;
            loc.Name = txtLocName.Text.Trim().ToUpper();
            loc.LocAddress.Address = txtAddress.Text.Trim().ToUpper();
            loc.LocAddress.City = txtCity.Text.Trim().ToUpper();
            loc.LocAddress.Pin = txtPin.Text.Trim().ToUpper();
            loc.Abbreviation = txtAbbr.Text.Trim().ToUpper();
            loc.Phone = txtPhone.Text.Trim().ToUpper();
            loc.ManagerId = Convert.ToInt32(ddlManager.SelectedValue);

            if (chkActive.Checked)
                loc.IsActive = 'Y';
            else
                loc.IsActive = 'N';
        }

        #endregion
    }
}