using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSR.BLL;
using DSR.Utilities;
using DSR.Common;

namespace DSR.WebApp.Security
{
    public partial class AddEditUser : System.Web.UI.Page
    {
        #region Private Member Variables

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateRole();
                PopulateLocation();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void PopulateRole()
        {
            CommonBLL commonBll = new CommonBLL();
            List<IRole> lstRole = commonBll.GetRole();
            GeneralFunctions.PopulateDropDownList(ddlRole, lstRole, "Id", "Name", true);
        }

        private void PopulateLocation()
        {
            CommonBLL commonBll = new CommonBLL();
            List<ILocation> lstLoc = commonBll.GetLocationList();
            GeneralFunctions.PopulateDropDownList(ddlLoc, lstLoc, "Id", "Name", true);
        }

        #endregion
    }
}