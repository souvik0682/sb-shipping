using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.BLL;
using DSR.Utilities;
using DSR.Common;
using DSR.Utilities.ResourceManager;
using System.Configuration;
using System.Globalization;
using DSR.Entity;

namespace DSR.WebApp.Security
{
    public partial class CustomerAssign : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = false;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvwList.PageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            LoadData();
            upList.Update();
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

        protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 8);

                e.Row.Cells[0].Text = ((gvwList.PageSize * gvwList.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "NewUserName"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ExistingUserName"));

                if (Convert.ToChar(DataBinder.Eval(e.Row.DataItem, "AssignType")) == 'P')
                {
                    e.Row.Cells[3].Text = "Permanent";
                }
                else if (Convert.ToChar(DataBinder.Eval(e.Row.DataItem, "AssignType")) == 'T')
                {
                    e.Row.Cells[3].Text = "Temporary";
                }

                if (DataBinder.Eval(e.Row.DataItem, "StartDate") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "StartDate") != null)
                    e.Row.Cells[4].Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "StartDate"), _culture).ToString(Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]));

                if (DataBinder.Eval(e.Row.DataItem, "EndDate") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "EndDate") != null)
                    e.Row.Cells[5].Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "EndDate"), _culture).ToString(Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                if (_hasEditAccess)
                {
                    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00010") + "');";
                }
                else
                {
                    btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
                    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
                }
            }
        }

        protected void gvwList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditData")
            {
                GetData(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteData(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwList.PageIndex = e.NewPageIndex;
            LoadData();
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
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

                if (user.UserRole.Id != (int)UserRole.Manager)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }

                _hasEditAccess = true;
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                ShowEndDate(false);
            }
        }

        private void ShowEndDate(bool isVisible)
        {
            if (!isVisible)
            {
                lblFromDt.Text = "From:";
                txtEndDt.Style["display"] = "none";
                lblEndDt.Style["display"] = "none";
                spnEndDt.Style["display"] = "none";
            }
            else
            {
                lblFromDt.Text = "Start Date:";
                txtEndDt.Style["display"] = "";
                lblEndDt.Style["display"] = "";
            }
        }

        private void PopulateSalesExecutive()
        {
            CommonBLL commonBll = new CommonBLL();
            GeneralFunctions.PopulateDropDownList<IUser>(ddlNew, commonBll.GetSalesExecutive(_userId), "Id", "UserFullName", Constants.DROPDOWNLIST_ALL_TEXT);
            GeneralFunctions.PopulateDropDownList<IUser>(ddlExisting, commonBll.GetSalesExecutive(_userId), "Id", "UserFullName", Constants.DROPDOWNLIST_ALL_TEXT);
        }

        private void LoadData()
        {
            gvwList.DataSource = new CommonBLL().GetAssignedCustomer();
            gvwList.DataBind();
        }

        private void GetData(int id)
        {
            ICustomerAssign cust = new CommonBLL().GetAssignedCustomer(id);

            if (!ReferenceEquals(cust, null))
            {
                hdnId.Value = cust.Id.ToString();
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

                upAdd.Update();
            }
        }

        private void SaveData()
        {
            ICustomerAssign cust = new CustomerAssignEntity();
            BuildEntity(cust);
            CommonBLL commonBll = new CommonBLL();
            commonBll.SaveAssignedCustomer(cust, _userId);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00056") + "');</script>", false);
        }

        private void DeleteData(int id)
        {

        }

        private void BuildEntity(ICustomerAssign cust)
        {
            cust.ExistingUserId = Convert.ToInt32(ddlExisting.SelectedValue);
            cust.NewUserId = Convert.ToInt32(ddlNew.SelectedValue);
            cust.AssignType = Convert.ToChar(ddlType.SelectedValue);
            cust.StartDate = Convert.ToDateTime(txtStartDt.Text, _culture);

            if (cust.AssignType == 'T')
                cust.EndDate = Convert.ToDateTime(txtEndDt.Text, _culture);
        }
        #endregion
    }
}