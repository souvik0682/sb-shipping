using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.BLL;
using DSR.Utilities;

namespace DSR.WebApp
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string baseException = string.Empty;
            string userDetail = string.Empty;
            bool isSuccess = false;
            bool isHttpRequestValidationException = false;
           
            if (!IsPostBack)
            {
                try
                {
                    // ** This page is called whenever there is an unhandled exception
                    //    which is trapped by the Application_Error in Global.asax
                    if (Session[Constants.SESSION_ERROR] != null)
                    {
                        Exception ex = ((Exception)(Session[Constants.SESSION_ERROR]));

                        //Chech whether the exception is raised due to invalid HttpRequest or not.
                        isHttpRequestValidationException = (ex.GetType() == typeof(HttpRequestValidationException)) ? true : false;

                        if (ex.GetBaseException() != null)
                            baseException = ex.GetBaseException().ToString();

                        try
                        {
                            CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                        }
                        catch
                        {
                            //Consume the exception
                        }

                        //Encode the error if the exception is raised due to invalid HttpRequest.
                        if (isHttpRequestValidationException)
                            baseException = HttpUtility.HtmlEncode(baseException);

                        isSuccess = true;
                    }
                }
                finally
                {
                    if (isSuccess)
                    {
                        lblErrMessage.Text = string.Format("The system has encountered an unhandled exception and could not complete your request. <br/>Please contact the system administrator.");
                        //lblErrMessage.Text = string.Format("The system has encountered an unhandled exception and could not complete your request. <br/>Please contact the system administrator.<br/><br/><br/><b>Error Description:</b><br/><br/>" + baseException);
                    }
                    else
                    {
                        lblErrMessage.Text = string.Format("The system has encountered an unhandled exception and could not complete your request. <br/>Please contact the system administrator.");
                    }
                }
            }
        }
    }
}