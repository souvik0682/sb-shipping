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
using System.Globalization;
using System.IO;
using System.Text;

namespace DSR.WebApp.Security
{
    public partial class ImportData : System.Web.UI.Page
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
            RetrieveParameters();
            SetAttributes();

            if (!IsPostBack)
            {
                LoadCustomer();
                LoadShipSoftData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<ShipSoftEntity> lstShipSoft = new List<ShipSoftEntity>();
            int tranId = 0;

            for (int index = 0; index < gvwData.Rows.Count; index++)
            {
                CheckBox chkSel = (CheckBox)gvwData.Rows[index].FindControl("chkSel");
                HiddenField hdnId = (HiddenField)gvwData.Rows[index].FindControl("hdnId");

                if (!ReferenceEquals(chkSel, null) && !ReferenceEquals(hdnId, null))
                {
                    if (chkSel.Checked)
                    {
                        tranId = 0;

                        int.TryParse(hdnId.Value, out tranId);

                        if (tranId > 0)
                        {
                            ShipSoftEntity shipSoft = new ShipSoftEntity();
                            shipSoft.TranId = tranId;
                            lstShipSoft.Add(shipSoft);
                        }
                    }
                }
            }

            if (lstShipSoft.Count > 0)
            {
                new CommonBLL().TagCustomer(lstShipSoft, Convert.ToInt32(ddlCust.SelectedValue), _userId);
                LoadShipSoftData();
                GeneralFunctions.RegisterAlertScript(this, ResourceManager.GetStringWithoutName("ERR00005"));
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00005") + "');</script>", false);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LoadShipSoftData();
            //upData.Update();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //LoadShipSoftData();
            //upArea.Update();

            if (fuShipSoft.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fuShipSoft.FileName);
                List<ShipSoftEntity> lstShipSoft = new List<ShipSoftEntity>();

                if (fileExt.ToUpper() == ".BLA")
                {
                    using (StreamReader reader = new StreamReader(fuShipSoft.FileContent, Encoding.Default))
                    {
                        while (!reader.EndOfStream)
                        {
                            string str = reader.ReadLine().Replace((char)29, '|');

                            string[] abc = str.Split('|');

                            if (abc.Length > 8)
                            {
                                try
                                {
                                    ShipSoftEntity shipSoft = new ShipSoftEntity();
                                    shipSoft.LocationName = Convert.ToString(abc[0]);
                                    shipSoft.ProspectName = Convert.ToString(abc[1]);
                                    shipSoft.BookingNo = Descramble(Convert.ToString(abc[2]));
                                    shipSoft.BLANumber = Descramble(Convert.ToString(abc[3]));
                                    shipSoft.VesselVoyage = Descramble(Convert.ToString(abc[4]));
                                    shipSoft.ShipperName = Descramble(Convert.ToString(abc[5]));
                                    shipSoft.PortName = Descramble(Convert.ToString(abc[6]));
                                    shipSoft.TEU = Convert.ToInt32(Descramble(Convert.ToString(abc[7])));
                                    shipSoft.FEU = Convert.ToInt32(Descramble(Convert.ToString(abc[8])));

                                    shipSoft.SOBDate = Convert.ToDateTime(Descramble(Convert.ToString(abc[9], _culture)));
                                    lstShipSoft.Add(shipSoft);
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }

                    if (lstShipSoft.Count > 0)
                    {
                        int dupCount = 0;
                        int rowsAffected = 0;
                        new CommonBLL().SaveShipSoft(lstShipSoft, _userId, out rowsAffected, out dupCount);
                        string message = "Total Number of Records Processed: " + Convert.ToString(lstShipSoft.Count) + "\\r\\n";
                        message += "Total Number of Records Up-loaded: " + rowsAffected.ToString() + "\\r\\n";
                        message += "Total Number of Records Rejected: " + dupCount.ToString();
                        GeneralFunctions.RegisterAlertScript(this, message);
                    }
                }
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, ResourceManager.GetStringWithoutName("ERR00052"));
            }
        }

        //private string test(string arg)
        //{
        //    int nInStrLen = 0;
        //    int cNextChar = 0;
        //    string cOutString = "";

        //    if (!string.IsNullOrEmpty(arg))
        //    {
        //        nInStrLen = arg.Length;

        //        for (int index = 1; index < nInStrLen; index++)
        //        {
        //            byte[] str = Encoding.ASCII.GetBytes(arg.Substring(index * 1, 1));
        //            cNextChar = str[0];
        //            cOutString = cOutString + (char)((cNextChar / 2));
        //        }
        //    }

        //    return cOutString;
        //}

        private string Descramble(string cInString)
        {
            int nInStrLen = 0;
            char cNextChar;
            string cOutString = string.Empty;

            if (!string.IsNullOrEmpty(cInString))
            {
                nInStrLen = cInString.Length;

                for (int nCounter = 0; nCounter < nInStrLen; nCounter++)
                {
                    cNextChar = Convert.ToChar(cInString.Substring(nCounter, 1));
                    cOutString = cOutString + Convert.ToString((char)(cNextChar - 96));
                }
            }

            return cInString;
        }

        private string ConvertUnicodeToAscii(string unicodeString)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.UTF8;

            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            // This is a slightly different approach to converting to illustrate
            // the use of GetCharCount/GetChars.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            // Display the strings created before and after the conversion.
            return asciiString;
        }
        //private string Descramble_old(string cInString)
        //{
        //    int nInStrLen = 0;
        //    char cNextChar;
        //    string cOutString = string.Empty;

        //    if (!string.IsNullOrEmpty(cInString))
        //    {
        //        nInStrLen = cInString.Length;

        //        for (int nCounter = 0; nCounter < nInStrLen; nCounter++)
        //        {
        //            cNextChar = Convert.ToChar(cInString.Substring(nInStrLen - nCounter - 1, 1));
        //            cOutString = cOutString + Convert.ToString((char)(cNextChar / 2));
        //        }
        //    }

        //    return cOutString;
        //}

        protected void gvwData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvwData.PageIndex = e.NewPageIndex;
            //LoadShipSoftData();
        }

        protected void gvwData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 12);

                //ScriptManager sManager = ScriptManager.GetCurrent(this);

                //e.Row.Cells[0].Text = ((gvwData.PageSize * gvwData.PageIndex) + e.Row.RowIndex + 1).ToString();
                ((HiddenField)e.Row.FindControl("hdnId")).Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TranId"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocationName"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ProspectName"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BookingNo"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLANumber"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselVoyage"));
                e.Row.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ShipperName"));
                e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PortName"));
                e.Row.Cells[8].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TEU"));
                e.Row.Cells[9].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FEU"));

                if (DataBinder.Eval(e.Row.DataItem, "SOBDate") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "SOBDate") != null)
                    e.Row.Cells[10].Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "SOBDate"), _culture).ToString(Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]));

                e.Row.Cells[11].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CustomerName"));

                //// Edit link
                //ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                //btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00008");
                //btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TranId"));

                ////Delete link
                //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                //btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TranId"));

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

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvwData.PageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            LoadShipSoftData();
            //upData.Update();
        }

        protected void rblTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCust.SelectedIndex = -1;
            LoadShipSoftData();
            //upData.Update();
        }

        protected void ddlCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadShipSoftData();
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

        private void RetrieveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                rblTag.SelectedValue = "1";
                //txtWMEArea.WatermarkText = ResourceManager.GetStringWithoutName("ERR00016");
                //txtWMELoc.WatermarkText = ResourceManager.GetStringWithoutName("ERR00018");
                btnCancel.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00050") + "')";
                rfvCust.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00051");
                gvwData.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                gvwData.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }

            if (!_hasEditAccess)
            {
                btnImport.Visible = false;
                btnCancel.Visible = false;
                btnSave.Visible = false;
            }
        }

        private void LoadShipSoftData()
        {
            bool isTagged = (rblTag.SelectedValue == "1") ? true : false;
            gvwData.DataSource = new CommonBLL().GetShipSoftData(Convert.ToInt32(ddlCust.SelectedValue), isTagged);
            gvwData.DataBind();
        }

        private void LoadCustomer()
        {
            GeneralFunctions.PopulateDropDownList<ICustomer>(ddlCust, new CommonBLL().GetCustomerByUser(_userId), "Id", "Name", true);
        }


        #endregion
    }
}