using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.BLL;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities;
using DSR.Utilities.ResourceManager;

namespace DSR.WebApp.Security
{
    public partial class AddEditCustomerAssign : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _custAssignId = 0;
        //private bool _hasEditAccess = false;
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateSalesExecutive();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                SaveData();
                Response.Redirect("~/Security/CustomerAssign.aspx");
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "P")
            {
                ShowEndDate(false);
            }
            else
            {
                ShowEndDate(true);
            }
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _custAssignId);
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

                switch (user.UserRole.Id)
                {
                    case (int)UserRole.Admin:
                    case (int)UserRole.Management:
                    case (int)UserRole.Manager:
                        break;
                    default:
                        Response.Redirect("~/Unauthorized.aspx");
                        break;
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void SetAttributes()
        {
            spnStartDt.Style["display"] = "none";
            spnEndDt.Style["display"] = "none";

            if (!IsPostBack)
            {
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('CustomerAssign.aspx','" + ResourceManager.GetStringWithoutName("ERR00046") + "')";
                rfvExisting.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00057");
                rfvNew.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00058");
                //spnStartDt.InnerText = ResourceManager.GetStringWithoutName("ERR00059");
                spnEndDt.InnerText = ResourceManager.GetStringWithoutName("ERR00059");
                ShowEndDate(false);
            }
        }

        private void ShowEndDate(bool isVisible)
        {
            if (!isVisible)
            {
                lblFromDt.Text = "From:";
                trEndDt.Style["display"] = "none";
                spnEndDt.Style["display"] = "none";
            }
            else
            {
                lblFromDt.Text = "Start Date:";
                trEndDt.Style["display"] = "";
            }
        }

        private void PopulateSalesExecutive()
        {
            CommonBLL commonBll = new CommonBLL();
            GeneralFunctions.PopulateDropDownList<IUser>(ddlNew, commonBll.GetSalesExecutive(_userId), "Id", "UserFullName", Constants.DROPDOWNLIST_DEFAULT_TEXT);
            GeneralFunctions.PopulateDropDownList<IUser>(ddlExisting, commonBll.GetSalesExecutive(_userId), "Id", "UserFullName", Constants.DROPDOWNLIST_DEFAULT_TEXT);
        }

        private void LoadData()
        {
            ICustomerAssign cust = new CommonBLL().GetAssignedCustomer(_custAssignId);

            if (!ReferenceEquals(cust, null))
            {
                ddlExisting.SelectedValue = cust.ExistingUserId.ToString();
                ddlNew.SelectedValue = cust.NewUserId.ToString();
                ddlType.SelectedValue = Convert.ToString(cust.AssignType);
                txtStartDt.Text = cust.StartDate.ToString(ConfigurationManager.AppSettings["DateFormat"]);

                if (cust.AssignType == 'P')
                {
                    ShowEndDate(false);
                }
                else
                {
                    if (cust.EndDate.HasValue)
                        txtEndDt.Text = cust.EndDate.Value.ToString(ConfigurationManager.AppSettings["DateFormat"]);

                    ShowEndDate(true);
                }
            }
        }

        private void SaveData()
        {
            ICustomerAssign cust = new CustomerAssignEntity();
            BuildEntity(cust);
            CommonBLL commonBll = new CommonBLL();
            commonBll.SaveAssignedCustomer(cust, _userId);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00056") + "');</script>", false);
        }

        private void BuildEntity(ICustomerAssign cust)
        {
            cust.Id = _custAssignId;
            cust.ExistingUserId = Convert.ToInt32(ddlExisting.SelectedValue);
            cust.NewUserId = Convert.ToInt32(ddlNew.SelectedValue);
            cust.AssignType = Convert.ToChar(ddlType.SelectedValue);
            cust.StartDate = Convert.ToDateTime(txtStartDt.Text, _culture);

            if (cust.AssignType == 'T')
                cust.EndDate = Convert.ToDateTime(txtEndDt.Text, _culture);
        }

        private bool ValidateData()
        {
            bool isValid = true;

            if (ddlType.SelectedValue == "P")
            {
                if (txtStartDt.Text == string.Empty)
                {
                    spnStartDt.InnerText = ResourceManager.GetStringWithoutName("ERR00059");
                    spnStartDt.Style["display"] = "";
                    isValid = false;
                }
            }

            if (ddlType.SelectedValue == "T")
            {
                if (txtStartDt.Text == string.Empty)
                {
                    spnStartDt.InnerText = ResourceManager.GetStringWithoutName("ERR00059");
                    spnStartDt.Style["display"] = "";
                    isValid = false;
                }

                if (txtEndDt.Text == string.Empty)
                {
                    spnEndDt.Style["display"] = "";
                    isValid = false;
                }
            }

            if (txtStartDt.Text != string.Empty)
            {
                DateTime startDt = Convert.ToDateTime(txtStartDt.Text, _culture);

                if (DateTime.Compare(startDt, DateTime.Now.Date) == -1)
                {
                    spnStartDt.InnerText = ResourceManager.GetStringWithoutName("ERR00064");
                    spnStartDt.Style["display"] = "";
                    isValid = false;
                }
            }

            return isValid;
        }

        #endregion
    }
}