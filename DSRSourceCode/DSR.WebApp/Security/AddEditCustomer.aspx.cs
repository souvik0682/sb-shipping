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
    public partial class AddEditCustomer : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _custId = 0;

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
            SaveCustomer();
        }

        protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateArea(Convert.ToInt32(ddlLoc.SelectedValue), txtPin.Text.Trim());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateArea(Convert.ToInt32(ddlLoc.SelectedValue), txtPin.Text.Trim());
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageCustomer.aspx','" + ResourceManager.GetStringWithoutName("ERR00046") + "')";
                txtAddress.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 200)";
                txtProfile.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 500)";

                revPhone1.ValidationExpression = Constants.PHONE_REGX_EXP;
                revPhone2.ValidationExpression = Constants.PHONE_REGX_EXP;
                revContactMob1.ValidationExpression = Constants.PHONE_REGX_EXP;
                revContactMob2.ValidationExpression = Constants.PHONE_REGX_EXP;
                revEmail1.ValidationExpression = Constants.EMAIL_REGX_EXP;
                revEmail2.ValidationExpression = Constants.EMAIL_REGX_EXP;

                rfvGroup.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00024");
                rfvLoc.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00025");
                rfvArea.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00026");
                rfvCustType.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00027");
                rfvCorpLoc.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00028");
                rfvName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00029");
                rfvAddr.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00030");
                rfvPhone1.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00031");
                rfvPerson1.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00032");
                rfvDesig1.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00033");
                rfvContactMob1.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00034");
                revEmail1.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00023");
                revEmail2.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00023");
                rfvExecutive.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00045");
                revPhone1.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00047");
                revPhone2.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00047");
                revContactMob1.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00048");
                revContactMob2.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00048");
                ddlCorpLoc.SelectedValue = "L";
            }

            if (_custId == -1)
            {
                chkActive.Checked = true;
                chkActive.Enabled = false;
            }
        }

        private void PopulateControls()
        {
            CommonBLL commonBll = new CommonBLL();
            GeneralFunctions.PopulateDropDownList<IGroupCompany>(ddlGroup, commonBll.GetActiveGroupCompany(), "Id", "Name", true);
            GeneralFunctions.PopulateDropDownList<ILocation>(ddlLoc, commonBll.GetActiveLocation(), "Id", "Name", true);
            GeneralFunctions.PopulateDropDownList<ICustomerType>(ddlCustType, commonBll.GetActiveCustomerType(), "Id", "Name", true);
            GeneralFunctions.PopulateDropDownList<IUser>(ddlExecutive, commonBll.GetSalesExecutive(), "Id", "UserFullName", true);
            PopulateArea(0, string.Empty);
        }

        private void PopulateArea(int locId, string pinCode)
        {
            List<IArea> lstArea = new CommonBLL().GetAreaByLocationAndPinCode(locId, pinCode);
            ddlArea.Items.Clear();

            if (lstArea.Count > 0)
            {
                GeneralFunctions.PopulateDropDownList<IArea>(ddlArea, lstArea, "Id", "Name", true);
            }
            else
            {
                ddlArea.Items.Add(new ListItem(Constants.DROPDOWNLIST_DEFAULT_TEXT, Constants.DROPDOWNLIST_DEFAULT_VALUE));
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _custId);
            }
        }

        private void CheckUserAccess()
        {
            chkActive.Enabled = false;

            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }

                if (user.UserRole.Id == (int)UserRole.Admin)
                {
                    chkActive.Enabled = true;
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (_custId == 0)
                Response.Redirect("~/Security/ManageCustomer.aspx");
        }

        private void LoadData()
        {
            ICustomer cust = new CommonBLL().GetCustomer(_custId);

            if (!ReferenceEquals(cust, null))
            {
                txtName.Text = cust.Name;

                if (!ReferenceEquals(cust.Group, null))
                {
                    ddlGroup.SelectedValue = Convert.ToString(cust.Group.Id);
                }

                if (!ReferenceEquals(cust.Location, null))
                {
                    ddlLoc.SelectedValue = Convert.ToString(cust.Location.Id);

                    if (!ReferenceEquals(cust.Address, null))
                    {
                        PopulateArea(cust.Location.Id,cust.Address.Pin);
                    }
                    else
                    {
                        PopulateArea(cust.Location.Id, string.Empty);
                    }
                }

                if (!ReferenceEquals(cust.Area, null))
                {
                    ddlArea.SelectedValue = Convert.ToString(cust.Area.Id);
                }

                if (!ReferenceEquals(cust.CustType, null))
                {
                    ddlCustType.SelectedValue = Convert.ToString(cust.CustType.Id);
                }

                ddlCorpLoc.SelectedValue = Convert.ToString(cust.CorporateOrLocal);
                txtName.Text = cust.Name;

                if (!ReferenceEquals(cust.Address, null))
                {
                    txtAddress.Text = cust.Address.Address;
                    txtCity.Text = cust.Address.City;
                    txtPin.Text = cust.Address.Pin;
                }

                txtPhone1.Text = cust.Phone1;
                txtPhone2.Text = cust.Phone2;

                if (!ReferenceEquals(cust.ContactPerson1, null))
                {
                    txtPerson1.Text = cust.ContactPerson1.Name;
                    txtDesig1.Text = cust.ContactPerson1.Designation;
                    txtContactMob1.Text = cust.ContactPerson1.Mobile;
                    txtEmail1.Text = cust.ContactPerson1.EmailId;
                }

                if (!ReferenceEquals(cust.ContactPerson2, null))
                {
                    txtPerson2.Text = cust.ContactPerson2.Name;
                    txtDesig2.Text = cust.ContactPerson2.Designation;
                    txtContactMob2.Text = cust.ContactPerson2.Mobile;
                    txtEmail2.Text = cust.ContactPerson2.EmailId;
                }

                txtProfile.Text = cust.CustomerProfile;

                if (cust.SalesExecutiveId.HasValue)
                    ddlExecutive.SelectedValue = Convert.ToString(cust.SalesExecutiveId.Value);

                txtPan.Text = cust.PAN;
                txtTan.Text = cust.TAN;
                txtBin.Text = cust.BIN;
                txtIec.Text = cust.IEC;

                if (cust.IsActive == 'Y')
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;
            }
        }

        private void SaveCustomer()
        {
            CommonBLL commonBll = new CommonBLL();
            ICustomer cust = new CustomerEntity();
            string message = string.Empty;
            BuildCustomerEntity(cust);
            message = commonBll.SaveCustomer(cust, _userId);

            if (message == string.Empty)
            {
                Response.Redirect("~/Security/ManageCustomer.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, message);
            }
        }

        private void BuildCustomerEntity(ICustomer cust)
        {
            cust.Initialize();
            cust.Id = _custId;
            cust.Name = txtName.Text;
            cust.Group.Id = Convert.ToInt32(ddlGroup.SelectedValue);
            cust.Location.Id = Convert.ToInt32(ddlLoc.SelectedValue);
            cust.Area.Id = Convert.ToInt32(ddlArea.SelectedValue);
            cust.CustType.Id = Convert.ToInt32(ddlCustType.SelectedValue);
            cust.CorporateOrLocal = Convert.ToChar(ddlCorpLoc.SelectedValue);
            cust.Address.Address = txtAddress.Text.Trim();
            cust.Address.City = txtCity.Text.Trim();
            cust.Address.Pin = txtPin.Text.Trim();
            cust.Phone1 = txtPhone1.Text.Trim();
            cust.Phone2 = txtPhone2.Text.Trim();
            cust.ContactPerson1.Name = txtPerson1.Text.Trim();
            cust.ContactPerson1.Designation = txtDesig1.Text.Trim();
            cust.ContactPerson1.Mobile = txtContactMob1.Text.Trim();
            cust.ContactPerson1.EmailId = txtEmail1.Text.Trim();
            cust.ContactPerson2.Name = txtPerson2.Text.Trim();
            cust.ContactPerson2.Designation = txtDesig2.Text.Trim();
            cust.ContactPerson2.Mobile = txtContactMob2.Text.Trim();
            cust.ContactPerson2.EmailId = txtEmail2.Text.Trim();
            cust.CustomerProfile = txtProfile.Text.Trim();

            if (ddlExecutive.SelectedValue != "0")
                cust.SalesExecutiveId = Convert.ToInt32(ddlExecutive.SelectedValue);

            cust.PAN = txtPan.Text.Trim();
            cust.TAN = txtTan.Text.Trim();
            cust.BIN = txtBin.Text.Trim();
            cust.IEC = txtIec.Text.Trim();

            if (chkActive.Checked)
                cust.IsActive = 'Y';
            else
                cust.IsActive = 'N';
        }

        #endregion
    }
}