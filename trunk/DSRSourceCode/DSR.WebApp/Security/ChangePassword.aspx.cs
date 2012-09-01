using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.BLL;
using DSR.Utilities;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities.ResourceManager;

namespace DSR.WebApp.Security
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();
        }

        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            string oldPassword = Utilities.Cryptography.Encryption.Encrypt(txtOldPwd.Text.Trim());
            string newPassword = Utilities.Cryptography.Encryption.Encrypt(txtNewPwd.Text.Trim());

            IUser user = new UserEntity();
            user.Id = _userId;
            user.Password = oldPassword;
            user.NewPassword = newPassword;
            isSuccess = new UserBLL().ChangePassword(user);

            if (isSuccess)
            {
                Session.Abandon();
                Page.ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script>javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00065") + "');window.location.href='../Login.aspx'</script>");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, ResourceManager.GetStringWithoutName("ERR00066"));
            }
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            if (!ReferenceEquals(Request.QueryString["id"], null) && Convert.ToString(Request.QueryString["id"]) != string.Empty)
            {
                int.TryParse(Convert.ToString(Request.QueryString["id"]), out _userId);
            }
            else
            {
                _userId = UserBLL.GetLoggedInUserId();
            }
        }

        private void CheckUserAccess()
        {
            if (_userId == 0)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                rfvOldPwd.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00067");
                rfvNewPwd.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00068");
                rfvRePwd.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00069");
                cvRePwd.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00070");
            }
        }

        #endregion
    }
}