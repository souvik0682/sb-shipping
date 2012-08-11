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
using DSR.Common;
using System.Configuration;

namespace DSR.WebApp.Security
{
    public partial class ManageCustomer : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private int _locId = 0;
        private bool _hasEditAccess = true;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                SetDefaultSearchCriteria();
                LoadCustomer();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCustomer();
            upCust.Update();
        }

        protected void gvwCust_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwCust.PageIndex = e.NewPageIndex;
            LoadCustomer();
        }
        protected void gvwCust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                if (ViewState[Constants.SORT_EXPRESSION] == null)
                {
                    ViewState[Constants.SORT_EXPRESSION] = e.CommandArgument.ToString();
                    ViewState[Constants.SORT_DIRECTION] = "ASC";
                }
                else
                {
                    if (ViewState[Constants.SORT_EXPRESSION].ToString() == e.CommandArgument.ToString())
                    {
                        if (ViewState[Constants.SORT_DIRECTION].ToString() == "ASC")
                            ViewState[Constants.SORT_DIRECTION] = "DESC";
                        else
                            ViewState[Constants.SORT_DIRECTION] = "ASC";
                    }
                    else
                    {
                        ViewState[Constants.SORT_DIRECTION] = "ASC";
                        ViewState[Constants.SORT_EXPRESSION] = e.CommandArgument.ToString();
                    }
                }

                LoadCustomer();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteCustomer(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwCust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 7);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                string corporateOrLocal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CorporateOrLocal"));
                string custLocId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Location.Id"));
                bool canEdit = true;

                if (_roleId == (int)UserRole.Manager || _roleId == (int)UserRole.SalesExecutive)
                {
                    if (corporateOrLocal == "C" && _locId.ToString() != custLocId)
                    {
                        canEdit = false;
                    }
                }

                e.Row.Cells[0].Text = ((gvwCust.PageSize * gvwCust.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Location.Name"));
                //e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));

                //SA Modify -- Souvik
                string strCustName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));

                if (strCustName.Length > 50)
                {
                    ((Label)e.Row.FindControl("lblName")).ToolTip = strCustName;
                    strCustName = strCustName.Substring(0, 50) + "..";
                }


                if (Convert.ToChar(DataBinder.Eval(e.Row.DataItem, "IsActive")) == 'Y')
                {
                    ((Label)e.Row.FindControl("lblName")).Text = strCustName;
                    ((Label)e.Row.FindControl("lblInActive")).Style["display"] = "none";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblName")).Text = strCustName;
                    ((Label)e.Row.FindControl("lblInActive")).Style["display"] = "";
                }


                string strGroupCompany = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Group.Name"));
                if (strGroupCompany.Length > 17)
                {
                    e.Row.Cells[3].ToolTip = strGroupCompany;
                    strGroupCompany = strGroupCompany.Substring(0, 17) + "..";
                }

                e.Row.Cells[3].Text = strGroupCompany;
                //EA Modify -- Souvik

                string strSalesPerson = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SalesExecutiveName"));
                if (strSalesPerson.Length > 17)
                {
                    e.Row.Cells[4].ToolTip = strSalesPerson;
                    strSalesPerson = strSalesPerson.Substring(0, 17) + "..";
                }

                e.Row.Cells[4].Text = strSalesPerson;

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                if (_hasEditAccess && canEdit)
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

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvwCust.PageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            LoadCustomer();
            upCust.Update();
        }

        #endregion

        #region Private Methods

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin && user.UserRole.Id != (int)UserRole.Manager && user.UserRole.Id != (int)UserRole.SalesExecutive)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            IUser user = new UserBLL().GetUser(_userId);

            if (!ReferenceEquals(user, null))
            {
                if (!ReferenceEquals(user.UserRole, null))
                {
                    _roleId = user.UserRole.Id;
                }

                if (!ReferenceEquals(user.UserLocation, null))
                {
                    _locId = user.UserLocation.Id;
                }
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                txtWMELoc.WatermarkText = ResourceManager.GetStringWithoutName("ERR00018");
                txtWMECust.WatermarkText = ResourceManager.GetStringWithoutName("ERR00022");
                txtWMEGr.WatermarkText = ResourceManager.GetStringWithoutName("ERR00021");
                gvwCust.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                gvwCust.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void LoadCustomer()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    CommonBLL commonBll = new CommonBLL();
                    gvwCust.DataSource = commonBll.GetAllCustomer(searchCriteria);
                    gvwCust.DataBind();
                }
            }
        }

        private void DeleteCustomer(int custId)
        {
            CommonBLL commonBll = new CommonBLL();
            commonBll.DeleteCustomer(custId, _userId);
            LoadCustomer();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Security/AddEditCustomer.aspx?id=" + encryptedId);
        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            int roleId = UserBLL.GetLoggedInUserRoleId();

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }
            else
            {
                //SA Modify -- Souvik
                sortExpression = "CustName";
                //EA Modify -- Souvik
                sortDirection = "ASC";
            }

            criteria.UserId = _userId;
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.CustomerName = (txtCustName.Text == ResourceManager.GetStringWithoutName("ERR00022")) ? string.Empty : txtCustName.Text.Trim();
            criteria.GroupName = (txtGrComp.Text == ResourceManager.GetStringWithoutName("ERR00021")) ? string.Empty : txtGrComp.Text.Trim();
            criteria.LocAbbr = (txtLoc.Text == ResourceManager.GetStringWithoutName("ERR00018")) ? string.Empty : txtLoc.Text.Trim();
            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void SetDefaultSearchCriteria()
        {
            SearchCriteria criteria = new SearchCriteria();

            //SA Modify -- Souvik
            //string sortExpression = "Location";
            string sortExpression = "CustName";
            //EA Modify -- Souvik
            string sortDirection = "ASC";

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;

            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        #endregion
    }
}