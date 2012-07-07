using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.Utilities.ResourceManager;

namespace DSR.WebApp
{
    public partial class Login : System.Web.UI.Page
    {
        #region Protected Event Handlers
                
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetAttributes();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateControl())
            {

            }
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            txtUserName.Focus();
            lblMsg.Visible = false;
            lblMsgUsername.Visible = false;
            lblMsgPassword.Visible = false;
            txtPassword.Attributes.Add("autocomplete", "off");
            rfvName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00001");
            rfvPwd.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00002"); 
        }

        private bool ValidateControl()
        {
            bool isValid = true;

            if (txtUserName.Text.Trim() == string.Empty)
            {
                isValid = false;
                lblMsgUsername.Visible = true;
                lblMsgUsername.Text = ResourceManager.GetStringWithoutName("ERR00001");
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                isValid = false;
                lblMsgPassword.Visible = true;
                lblMsgPassword.Text = ResourceManager.GetStringWithoutName("ERR00002");
            }

            return isValid;
        }

        #endregion
    }
}