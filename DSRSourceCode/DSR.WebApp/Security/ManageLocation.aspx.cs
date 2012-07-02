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
    public partial class ManageLocation : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDefaultSearchCriteria();
                LoadLocation();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            upLoc.Update();
        }

        protected void gvwLoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwLoc.PageIndex = e.NewPageIndex;
            LoadLocation();
        }
        protected void gvwLoc_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadLocation();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteLocation(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwLoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 7);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwLoc.PageSize * gvwLoc.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Abbreviation"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocAddress.City")) + "-" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocAddress.Pin"));
                //e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocalManager"));

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

        #endregion

        #region Private Methods

        private void LoadLocation()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    CommonBLL commonBll = new CommonBLL();
                    gvwLoc.DataSource = commonBll.GetAllLocationList();
                    gvwLoc.DataBind();
                }
            }
        }

        private void DeleteLocation(int locId)
        {
            CommonBLL commonBll = new CommonBLL();
            commonBll.DeleteLocation(locId, _userId);
            LoadLocation();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Security/AddEditLocation.aspx?id=" + encryptedId);
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