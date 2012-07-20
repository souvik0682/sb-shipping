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
using System.Data.OleDb;

namespace DSR.WebApp.Security
{
    public partial class ImportData : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                SetDefaultSearchCriteria();
                LoadArea();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //LoadArea();
            //upArea.Update();

            if (fuShipSoft.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fuShipSoft.FileName);

                if (fileExt == ".csv")
                {
                    OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + fuShipSoft + ";     Extended Properties=Excel 8.0");
                    try
                    {
                        OleDbCommand ocmd = new OleDbCommand("SELECT * FROM [AudioPR$]", oconn);
                        oconn.Open();
                        OleDbDataReader odr = ocmd.ExecuteReader();
                        string Device = "";
                        string Source = "";
                        string Reviewer = "";

                        while (odr.Read())
                        {
                            //Device = valid(odr, 0);
                            //Source = valid(odr, 1);
                            //InsertDataIntoSql(Device, Source, Reviewer, Datetime, Links, Content, Subject);
                        }

                        oconn.Close();
                    }
                    catch (Exception ee) { }
                    finally
                    {
                        //Label1.Text = "Data Inserted Successfully";
                    }
                }
            }
        }

        protected void gvwArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvwArea.PageIndex = e.NewPageIndex;
            //LoadArea();
        }
        protected void gvwArea_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadArea();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DelateArea(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwArea_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 5);

            //    ScriptManager sManager = ScriptManager.GetCurrent(this);

            //    e.Row.Cells[0].Text = ((gvwArea.PageSize * gvwArea.PageIndex) + e.Row.RowIndex + 1).ToString();
            //    e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Location.Name"));
            //    e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));
            //    e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PinCode"));

            //    // Edit link
            //    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
            //    btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
            //    btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

            //    //Delete link
            //    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
            //    btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
            //    btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

            //    if (_hasEditAccess)
            //    {
            //        btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00010") + "');";
            //    }
            //    else
            //    {
            //        btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
            //        btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
            //    }
            //}
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gvwArea.PageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            //LoadArea();
            //upArea.Update();
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

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    Response.Redirect("~/Unauthorized.aspx");
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
                //txtWMEArea.WatermarkText = ResourceManager.GetStringWithoutName("ERR00016");
                //txtWMELoc.WatermarkText = ResourceManager.GetStringWithoutName("ERR00018");
                //gvwArea.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                //gvwArea.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void LoadArea()
        {
            //if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            //{
            //    SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

            //    if (!ReferenceEquals(searchCriteria, null))
            //    {
            //        BuildSearchCriteria(searchCriteria);
            //        CommonBLL commonBll = new CommonBLL();
            //        gvwArea.DataSource = commonBll.GetAllArea(searchCriteria);
            //        gvwArea.DataBind();
            //    }
            //}
        }

        private void DelateArea(int areaId)
        {
            CommonBLL commonBll = new CommonBLL();
            commonBll.DeleteArea(areaId, _userId);
            LoadArea();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00006") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Security/AddEditArea.aspx?id=" + encryptedId);
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
                sortExpression = "Area";
                sortDirection = "ASC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            //criteria.AreaName = (txtArea.Text == ResourceManager.GetStringWithoutName("ERR00016")) ? string.Empty : txtArea.Text.Trim();
            //criteria.LocName = (txtLoc.Text == ResourceManager.GetStringWithoutName("ERR00018")) ? string.Empty : txtLoc.Text.Trim();

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