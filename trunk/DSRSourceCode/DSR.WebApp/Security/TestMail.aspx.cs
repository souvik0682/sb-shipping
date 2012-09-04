using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.BLL;
using System.Configuration;
using DSR.Utilities;

namespace DSR.WebApp.Security
{
    public partial class TestMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dvMsg.Style["display"] = "none";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string msgBody = @"This is a test mail. Please ignore it.";
                CommonBLL.SendMail(Convert.ToString(ConfigurationManager.AppSettings["Sender"]), Convert.ToString(ConfigurationManager.AppSettings["DisplayName"]), TextBox1.Text, string.Empty, "New account creation", msgBody, Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]));
                GeneralFunctions.RegisterAlertScript(this, "Mail successfully send");
            }
            catch (Exception ex)
            {
                dvMsg.Style["display"] = "";
                dvMsg.InnerText = "An error has occured: " + ex.Message + "<br/>" + ex.InnerException.ToString();
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string msgBody = @"This is a test mail. Please ignore it.";
                CommonBLL.SendMail(Convert.ToString(ConfigurationManager.AppSettings["Sender"]), Convert.ToString(ConfigurationManager.AppSettings["DisplayName"]), TextBox1.Text, string.Empty, "New account creation", msgBody, Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserAccount"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserPwd"]));
                GeneralFunctions.RegisterAlertScript(this, "Mail successfully send");
            }
            catch (Exception ex)
            {
                dvMsg.Style["display"] = "";
                dvMsg.InnerText = "An error has occured: " + ex.Message + "<br/>" + ex.InnerException.ToString();
            }

        }
    }
}