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
                LoadData();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvwList.PageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            LoadData();
            upList.Update();
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

                char assignType = Convert.ToChar(DataBinder.Eval(e.Row.DataItem, "AssignType"));
                DateTime startDt = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "StartDate"), _culture);

                if (assignType == 'P') // Permanent, cannot be deleted or edited
                {
                    btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00060") + "');return false;";
                    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00061") + "');return false;";
                }
                else
                {
                    if (DateTime.Compare(startDt, DateTime.Now.Date) == -1)
                    {
                        btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00062") + "');return false;";
                        btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00063") + "');return false;";
                    }
                    else
                    {
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
            }
        }

        protected void gvwList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditData")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
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
            upList.Update();
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
                gvwList.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                gvwList.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Security/AddEditCustomerAssign.aspx?id=" + encryptedId);
        }

        private void LoadData()
        {
            gvwList.DataSource = new CommonBLL().GetAssignedCustomer();
            gvwList.DataBind();
        }

        private void DeleteData(int id)
        {
            new CommonBLL().DeleteAssignedCustomer(id, _userId);
            LoadData();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        #endregion
    }
}