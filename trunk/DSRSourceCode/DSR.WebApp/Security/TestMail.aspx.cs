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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonBLL.SendMail("maa.system@benlineagencies.com", TextBox1.Text, string.Empty, "Test Mail", "This is a test mail", Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserAccount"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserPwd"]));
                GeneralFunctions.RegisterAlertScript(this, "Mail successfully send");
            }
            catch (Exception ex)
            {
                GeneralFunctions.RegisterAlertScript(this, ex.Message);
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                CommonBLL.SendMail("maa.system@benlineagencies.com", TextBox1.Text, string.Empty, "Test Mail", "This is a test mail", Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]));
                GeneralFunctions.RegisterAlertScript(this, "Mail successfully send");
            }
            catch (Exception ex)
            {
                GeneralFunctions.RegisterAlertScript(this, ex.Message);
            }

        }
    }
}