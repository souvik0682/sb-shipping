using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.Common;
using DSR.Utilities;
using DSR.BLL;
using DSR.Entity;
using DSR.Utilities.ResourceManager;
using System.Globalization;

namespace DSR.WebApp.Security
{
    public partial class DailySalesCallEntry : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _portId = 0;
        private List<ICommitment> commitmentDetails = new List<ICommitment>();
        DailySalesCallBLL objDailySalesCall;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateCallType();
                PopulateProspectFor();
                GetCustomer();

                LoadRecordForEdit();
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageDailySalesCall.aspx','" + ResourceManager.GetStringWithoutName("ERR00046") + "')";
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

            //if (_areaId == 0)
            //    Response.Redirect("~/Security/ManageArea.aspx");
        }

        private void PopulateCallType()
        {
            GeneralFunctions.PopulateDropDownList<ICallType>(ddlCallType, new CommonBLL().GetActiveCallType(), "Id", "Name", true);
            ddlCallType.SelectedValue = "3";
        }

        private void PopulateProspectFor()
        {
            GeneralFunctions.PopulateDropDownList<IProspect>(ddlProspectFor, new CommonBLL().GetAllProspect(), "Id", "Name", true);
        }

        private void GetCustomer()
        {
            GeneralFunctions.PopulateDropDownList<ICustomer>(ddlCustomer, new CommonBLL().GetActiveCustomer(), "Id", "Name", true);
        }

        private int IsValidDestination()
        {
            DailySalesCallBLL objDSC = new DailySalesCallBLL();

            return objDSC.ValidateDestination(txtDestination.Text.Trim());
        }

        private void AddCommitmentDetails()
        {
            ICommitment commitment = new CommitmentEntity();
            BuildCommitmentEntity(commitment);

            if (ViewState["CommittmentDetails"] != null)
                commitmentDetails = ViewState["CommittmentDetails"] as List<ICommitment>;

            commitmentDetails.Add(commitment);

            gvwSalesCall.DataSource = commitmentDetails;
            gvwSalesCall.DataBind();

            ViewState["CommittmentDetails"] = commitmentDetails;
            ClearCommitmentDetail();
        }

        private void ClearCommitmentDetail()
        {
            txtDestination.Text = "";
            txtFEU.Text = "";
            txtTEU.Text = "";
            txtWeekNo.Text = "";
            ViewState["CommitmentId"] = null;
        }

        private void EditCommitmentDetails()
        {
            ICommitment commitment;

            if (ViewState["CommittmentDetails"] != null)
                commitmentDetails = ViewState["CommittmentDetails"] as List<ICommitment>;

            commitment = commitmentDetails.Single(c => c.CommitmentId == Convert.ToInt32(ViewState["CommitmentId"]));
            commitmentDetails.Remove(commitment);


            commitment.WeekNo = Convert.ToInt32(txtWeekNo.Text.Trim());
            commitment.PortCode = txtDestination.Text.Trim();
            commitment.PortId = _portId;
            commitment.FEU = Convert.ToInt32(txtFEU.Text.Trim());
            commitment.TEU = Convert.ToInt32(txtTEU.Text.Trim());

            commitmentDetails.Add(commitment);
            ViewState["CommittmentDetails"] = commitmentDetails;
            RefreshGridView();
            ClearCommitmentDetail();
        }

        private void SaveDailySalesCall()
        {
            IDailySalesCall dailySales = new DailySalesCallEntity();
            BuildDailySalesEntity(dailySales);

            if (ValidateSave(dailySales))
            {
                List<ICommitment> commitments = ViewState["CommittmentDetails"] as List<ICommitment>;
                objDailySalesCall = new DailySalesCallBLL();
                objDailySalesCall.SaveDailySalesCallEntry(dailySales, commitments);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00005") + "');</script>", false);
            }
        }

        private void LoadRecordForEdit()
        {
            int CallId;

            if (Request.QueryString["CallId"] != null)
            {
                CallId = Convert.ToInt32(Request.QueryString["CallId"]);
                objDailySalesCall = new DailySalesCallBLL();

                IDailySalesCall objSalesCall = objDailySalesCall.GetDailySalesCallEntry(CallId);
                txtCallDate.Text = objSalesCall.CallDate.ToString("dd/MM/yyyy");
                ddlCallType.SelectedValue = objSalesCall.CallType.ToString();
                ddlCustomer.SelectedValue = objSalesCall.CustomerId.ToString();
                ddlProspectFor.SelectedValue = objSalesCall.ProspectId.ToString();

                if (objSalesCall.NextCallDate.HasValue)
                    txtNextCallDate.Text = objSalesCall.NextCallDate.Value.ToString("dd/MM/yyyy");

                txtRemarks.Text = objSalesCall.Remarks;

                List<ICommitment> lstCommitment = objDailySalesCall.GetCommitments(CallId);
                ViewState["CommittmentDetails"] = lstCommitment;
                RefreshGridView();
            }
        }

        private void RefreshGridView()
        {
            if (ViewState["CommittmentDetails"] != null)
                commitmentDetails = ViewState["CommittmentDetails"] as List<ICommitment>;

            gvwSalesCall.DataSource = commitmentDetails;
            gvwSalesCall.DataBind();
        }

        private void BuildCommitmentEntity(ICommitment commitment)
        {
            if (ViewState["CommID"] == null)
                commitment.CommitmentId = -1;
            else
                commitment.CommitmentId = Convert.ToInt32(ViewState["CommID"]) - 1;

            ViewState["CommID"] = commitment.CommitmentId;

            commitment.CallId = 1; // get call id after save header call section.
            commitment.CustomerId = Convert.ToInt32(ddlCustomer.SelectedValue);
            commitment.FEU = Convert.ToInt32(txtFEU.Text);
            commitment.PortCode = txtDestination.Text.Trim();
            commitment.PortId = _portId;
            commitment.TEU = Convert.ToInt32(txtTEU.Text);
            commitment.WeekNo = Convert.ToInt32(txtWeekNo.Text);
            commitment.UserId = _userId;
        }

        private void BuildDailySalesEntity(IDailySalesCall dailySales)
        {
            int CallId;
            dailySales.CallDate = GetDate(txtCallDate.Text.Trim());

            if (txtNextCallDate.Text.Trim() != string.Empty)
                dailySales.NextCallDate = GetDate(txtNextCallDate.Text.Trim());
            else
                dailySales.NextCallDate = null;

            //For Add
            dailySales.CallId = 0;

            //For Edit
            dailySales.CallType = Convert.ToInt32(ddlCallType.SelectedValue);
            dailySales.CustomerId = Convert.ToInt32(ddlCustomer.SelectedValue);
            dailySales.ProspectId = Convert.ToInt32(ddlProspectFor.SelectedValue);
            dailySales.Remarks = txtRemarks.Text;
            dailySales.UserId = ((IUser)Session[Constants.SESSION_USER_INFO]).Id;

            if (Request.QueryString["CallId"] != null)
            {
                //For Edit
                dailySales.ModifiedBy = _userId;
                CallId = Convert.ToInt32(Request.QueryString["CallId"]);
                dailySales.CallId = CallId;
            }
            else
            {
                //For Add
                dailySales.CallId = 0;
                dailySales.CreatedBy = ((IUser)Session[Constants.SESSION_USER_INFO]).Id; ;
            }
        }

        private DateTime GetDate(string date)
        {
            if (date != string.Empty)
            {
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd/MM/yyyy";
                dtfi.DateSeparator = "/";
                return Convert.ToDateTime(date, dtfi);
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        protected void btnAddToGrid_Click(object sender, EventArgs e)
        {
            if (ValidateAddToGrid())
            {
                _portId = IsValidDestination();

                if (_portId != -1)
                {
                    if (btnAddToGrid.Text == "Add Record")
                        AddCommitmentDetails();
                    else
                    {
                        EditCommitmentDetails();
                        btnAddToGrid.Text = "Add Record";
                    }
                    lblDestination.Text = "";
                }
                else
                {
                    lblDestination.Text = "Please enter valid Destination";
                }
            }
        }

        private void EditCommitment(int CommitmentId)
        {
            if (ViewState["CommittmentDetails"] != null)
                commitmentDetails = ViewState["CommittmentDetails"] as List<ICommitment>;

            ICommitment committment = commitmentDetails.Single(c => c.CommitmentId == CommitmentId);

            txtDestination.Text = committment.PortCode;
            txtFEU.Text = Convert.ToString(committment.FEU);
            txtTEU.Text = Convert.ToString(committment.TEU);
            txtWeekNo.Text = Convert.ToString(committment.WeekNo);

            ViewState["CommitmentId"] = CommitmentId;
            btnAddToGrid.Text = "Save Record";
        }

        private void DeleteCommitmentDetails(int CommitmentId)
        {
            if (ViewState["CommittmentDetails"] != null)
                commitmentDetails = ViewState["CommittmentDetails"] as List<ICommitment>;

            ICommitment committment = commitmentDetails.Single(c => c.CommitmentId == CommitmentId);

            commitmentDetails.Remove(committment);
            ViewState["CommittmentDetails"] = commitmentDetails;

            ClearCommitmentDetail();
            RefreshGridView();
        }

        protected void gvwSalesCall_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCommitment")
            {
                EditCommitment(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteCommitmentDetails(Convert.ToInt32(e.CommandArgument));
            }
        }
        protected void gvwSalesCall_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 9);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                //e.Row.Cells[0].Text = ((gvwSalesCall.PageSize * gvwUser.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "WeekNo"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PortCode"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TEU"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FEU"));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CommitmentId"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CommitmentId"));


                btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00010") + "');";

                //if (_hasEditAccess)
                //{
                //    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00010") + "');";
                //}
                //else
                //{
                //    btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
                //    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00009") + "');return false;";
                //}
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveDailySalesCall();
        }

        private bool ValidateSave(IDailySalesCall dailySales)
        {
            bool isValid = true;
            spnCallDate.Style["display"] = "none";
            spnNextCallDate.Style["display"] = "none";
            spnCallType.Style["display"] = "none";
            spnCustomer.Style["display"] = "none";
            spnProspect.Style["display"] = "none";

            if (dailySales.CallDate == DateTime.MinValue)
            {
                isValid = false;
                spnCallDate.Style["display"] = "";
            }
            else
            {
                if (!IsValidCallDate())
                {
                    isValid = false;
                    spnCallDate.InnerHtml = "Invalid Call Date";
                    spnCallDate.Style["display"] = "";
                }
            }

            if (dailySales.NextCallDate != DateTime.MinValue)
            {
                if (!IsValidNextCallDate())
                {
                    isValid = false;
                    spnNextCallDate.InnerHtml = "Next Call Date should be future date";
                    spnNextCallDate.Style["display"] = "";
                }
            }

            if (dailySales.CallType == 0)
            {
                isValid = false;
                spnCallType.Style["display"] = "";
            }

            if (dailySales.CustomerId == 0)
            {
                isValid = false;
                spnCustomer.Style["display"] = "";
            }

            if (dailySales.ProspectId == 0)
            {
                isValid = false;
                spnProspect.Style["display"] = "";
            }
            return isValid;
        }

        private bool ValidateAddToGrid()
        {
            lblWeek.Text = "";
            lblFEU.Text = "";
            lblTEU.Text = "";
            lblDestination.Text = "";

            bool isValid = true;
            int res = 0;

            if (txtWeekNo.Text.Trim() != string.Empty)
            {
                int.TryParse(txtWeekNo.Text.Trim(), out res);

                if (res == 0 || res > 52)
                {
                    lblWeek.Text = "Invalid Week Number";
                    isValid = false;
                }
                else if (!IsValidWeek(res))
                {
                    lblWeek.Text = "Should be current or future week";
                    isValid = false;
                }
            }
            else
            {
                lblWeek.Text = "Week Number can not be blank";
                isValid = false;
            }

            if (txtTEU.Text.Trim() != string.Empty)
            {
                int.TryParse(txtTEU.Text.Trim(), out res);

                if (res == 0)
                {
                    lblTEU.Text = "Invalid TEU";
                    isValid = false;
                }
            }
            else
            {
                lblTEU.Text = "TEU can not be blank";
                isValid = false;
            }

            if (txtFEU.Text.Trim() != string.Empty)
            {
                int.TryParse(txtFEU.Text.Trim(), out res);

                if (res == 0)
                {
                    lblFEU.Text = "Invalid FEU";
                    isValid = false;
                }
            }
            else
            {
                lblFEU.Text = "FEU can not be blank";
                isValid = false;
            }

            if (txtDestination.Text.Trim() == string.Empty)
            {
                lblDestination.Text = "Destination can not be blank";
                isValid = false;
            }

            return isValid;
        }

        //future date entry cannot be done. and from monday to sun day entry will be there
        private bool IsValidCallDate()
        {
            bool isValid = false;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
            DateTime callDT = DateTime.Parse(txtCallDate.Text.Trim(), culture, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime currentDT = DateTime.Now;

            //Future Date Checking
            if (callDT.Date <= currentDT.Date)
                isValid = true;

            //Back-Dated entry checking
            if (isValid)
            {
                int currentWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                DayOfWeek currentDay = currentDT.DayOfWeek;

                int callDateWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(callDT.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                DayOfWeek callDateDay = callDT.DayOfWeek;


                if ((currentWeek - 1) == callDateWeek)
                {
                    if (currentDay == DayOfWeek.Monday)
                    {
                        if (callDateDay == DayOfWeek.Monday || callDateDay == DayOfWeek.Tuesday ||
                            callDateDay == DayOfWeek.Wednesday || callDateDay == DayOfWeek.Thursday ||
                            callDateDay == DayOfWeek.Friday || callDateDay == DayOfWeek.Saturday ||
                            callDateDay == DayOfWeek.Sunday)
                            isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else if (currentWeek == callDateWeek)
                {
                    if (currentDay == DayOfWeek.Monday)
                    {
                        if (callDateDay == DayOfWeek.Monday)
                            isValid = true;
                    }
                    else if (currentDay == DayOfWeek.Tuesday)
                    {
                        if (callDateDay == DayOfWeek.Monday || callDateDay == DayOfWeek.Tuesday)
                            isValid = true;
                    }
                    else if (currentDay == DayOfWeek.Wednesday)
                    {
                        if (callDateDay == DayOfWeek.Monday || callDateDay == DayOfWeek.Tuesday ||
                            callDateDay == DayOfWeek.Wednesday)
                            isValid = true;
                    }
                    else if (currentDay == DayOfWeek.Thursday)
                    {
                        if (callDateDay == DayOfWeek.Monday || callDateDay == DayOfWeek.Tuesday ||
                            callDateDay == DayOfWeek.Wednesday || callDateDay == DayOfWeek.Thursday)
                            isValid = true;
                    }
                    else if (currentDay == DayOfWeek.Friday)
                    {
                        if (callDateDay == DayOfWeek.Monday || callDateDay == DayOfWeek.Tuesday ||
                            callDateDay == DayOfWeek.Wednesday || callDateDay == DayOfWeek.Thursday ||
                            callDateDay == DayOfWeek.Friday)
                            isValid = true;
                    }
                    else if (currentDay == DayOfWeek.Saturday)
                    {
                        if (callDateDay == DayOfWeek.Monday || callDateDay == DayOfWeek.Tuesday ||
                            callDateDay == DayOfWeek.Wednesday || callDateDay == DayOfWeek.Thursday ||
                            callDateDay == DayOfWeek.Friday || callDateDay == DayOfWeek.Saturday)
                            isValid = true;
                    }
                    else if (currentDay == DayOfWeek.Sunday)
                    {
                        if (callDateDay == DayOfWeek.Monday || callDateDay == DayOfWeek.Tuesday ||
                            callDateDay == DayOfWeek.Wednesday || callDateDay == DayOfWeek.Thursday ||
                            callDateDay == DayOfWeek.Friday || callDateDay == DayOfWeek.Saturday ||
                            callDateDay == DayOfWeek.Sunday)
                            isValid = true;
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        //next call date should be future date
        private bool IsValidNextCallDate()
        {
            bool isValid = false;

            if (txtNextCallDate.Text.Trim() != string.Empty)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                DateTime nextCallDT = DateTime.Parse(txtNextCallDate.Text.Trim(), culture, System.Globalization.DateTimeStyles.AssumeLocal);
                DateTime currentDT = DateTime.Now;

                if (nextCallDT.Date > currentDT.Date)
                    isValid = true;
            }
            else
            {
                isValid = true;
            }
            return isValid;
        }

        //committment week should be current/future week
        private bool IsValidWeek(int WeekNo)
        {
            bool isValid = true;
            int CurrentWeekNo = 0;

            DateTime currentDT = DateTime.Now;
            DayOfWeek currentWK = currentDT.DayOfWeek;

            CurrentWeekNo = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(currentDT, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (WeekNo < CurrentWeekNo)
                isValid = false;

            return isValid;
        }
    }
}