using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.Utilities;
using DSR.BLL;
using DSR.Utilities.ResourceManager;
using DSR.Entity;

namespace DSR.WebApp.Security
{
    public partial class ManageUser : System.Web.UI.Page
    {
        #region Private Member Variables       

        private bool _hasEditAccess = true;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            SetAttributes();

            if (!IsPostBack)
            {
                SetDefaultSearchCriteria();
                LoadUser();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(0);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvwUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwUser.PageIndex = e.NewPageIndex;
            LoadUser();
        }
        protected void gvwUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = e.CommandArgument.ToString();
                    ViewState["SortDirection"] = "ASC";
                }
                else
                {
                    if (ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                    {
                        if (ViewState["SortDirection"].ToString() == "ASC")
                            ViewState["SortDirection"] = "DESC";
                        else
                            ViewState["SortDirection"] = "ASC";
                    }
                    else
                    {
                        ViewState["SortDirection"] = "ASC";
                        ViewState["SortExpression"] = e.CommandArgument.ToString();
                    }
                }

                LoadUser();
            }
            else if (e.CommandName == "Remove")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 8);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwUser.PageSize * gvwUser.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CustomerRole.Name"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FirstName"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LastName"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CustomerLocation.Id"));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");


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

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            txtUserName.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtUserName.ClientID + "', 'Type Username')");
            txtUserName.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtUserName.ClientID + "', 'Type Username')");
            txtUserName.Text = "Type Username";


            txtFName.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtFName.ClientID + "', 'Type First Name')");
            txtFName.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtFName.ClientID + "', 'Type First Name')");
            txtFName.Text = "Type First Name";
        }

        private void LoadUser()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    UserBLL userBll = new UserBLL();
                    gvwUser.DataSource = userBll.GetAllUser(searchCriteria);
                    gvwUser.DataBind();
                }
            }
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Security/AddEditUser.aspx?id=" + encryptedId);
        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }
            else
            {
                sortExpression = "Name";
                sortDirection = "ASC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void SetDefaultSearchCriteria()
        {
            SearchCriteria criteria = new SearchCriteria();
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        #endregion
    }
}