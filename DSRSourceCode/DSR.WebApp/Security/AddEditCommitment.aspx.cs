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
    public partial class AddEditCommitment : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _commitmentId = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateLocation();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveArea();
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                //txtName.Attributes.Add("onkeypress", "ConvertToUpperCase();"); 
                //btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageArea.aspx','" + ResourceManager.GetStringWithoutName("ERR00046") + "')";
                //rfvName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00026");
                //rfvLoc.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00025");
                //rfvPin.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00049");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _commitmentId);
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

            if (_commitmentId == 0)
                Response.Redirect("~/Security/ManageArea.aspx");
        }

        private void PopulateLocation()
        {
            //GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, new CommonBLL().GetActiveLocation(), "Id", "Name", true);
        }

        private void LoadData()
        {
            ICommitment commitment = new CommonBLL().GetCommitment(_commitmentId);

            if (!ReferenceEquals(commitment, null))
            {
                //txtName.Text = area.Name;

                //if (!ReferenceEquals(area.Location, null))
                //    ddlLoc.SelectedValue = area.Location.Id.ToString();

                //txtPin.Text = area.PinCode;
            }
        }

        private void SaveArea()
        {
            CommonBLL commonBll = new CommonBLL();
            IArea area = new AreaEntity();
            string message = string.Empty;
            BuildAreaEntity(area);
            message = commonBll.SaveArea(area, _userId);

            if (message == string.Empty)
            {
                Response.Redirect("~/Security/ManageArea.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }
        }

        private void BuildAreaEntity(IArea area)
        {
            //area.Id = _areaId;
            //area.Name = txtName.Text;
            //area.PinCode = txtPin.Text;
            //area.Location.Id = Convert.ToInt32(ddlLoc.SelectedValue);
        }

        #endregion
    }
}