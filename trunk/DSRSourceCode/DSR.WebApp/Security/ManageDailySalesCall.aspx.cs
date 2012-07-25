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
using System.Globalization;

namespace DSR.WebApp.Security
{
    public partial class ManageDailySalesCall : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                LoadDSC();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDSC();
            upDSC.Update();
        }

        protected void gvwDSC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwDSC.PageIndex = e.NewPageIndex;
            LoadDSC();
        }
        protected void gvwDSC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteDSC(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwDSC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 8);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwDSC.PageSize * gvwDSC.PageIndex) + e.Row.RowIndex + 1).ToString();

                if (DataBinder.Eval(e.Row.DataItem, "CallDate") != DBNull.Value)
                    e.Row.Cells[1].Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "CallDate"), _culture).ToString(Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]));

                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CustomerName"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CallTypes"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Prospect"));

                if (DataBinder.Eval(e.Row.DataItem, "NextCallDate") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "NextCallDate") != null)
                    e.Row.Cells[5].Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "NextCallDate"), _culture).ToString(Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CallId"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CallId"));

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

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvwDSC.PageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            LoadDSC();
            upDSC.Update();
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

                if (user.UserRole.Id != (int)UserRole.Management)
                {
                    _hasEditAccess = true;
                }
                else
                {
                    _hasEditAccess = false;
                }
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
                gvwDSC.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                gvwDSC.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void LoadDSC()
        {
            CommonBLL commonBll = new CommonBLL();
            gvwDSC.DataSource = commonBll.GetDailySalesCallList();
            gvwDSC.DataBind();
        }

        private void DeleteDSC(int callId)
        {
            CommonBLL commonBll = new CommonBLL();
            commonBll.DeleteDailySalesCall(callId, _userId);
            LoadDSC();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            //string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());

            if (id > 0)
                Response.Redirect("~/Security/DailySalesCallEntry.aspx?CallId=" + id.ToString());
            else
                Response.Redirect("~/Security/DailySalesCallEntry.aspx");
        }

        #endregion
    }
}