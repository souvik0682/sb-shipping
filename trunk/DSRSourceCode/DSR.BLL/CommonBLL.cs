using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.DAL;
using DSR.Common;
using DSR.Entity;

namespace DSR.BLL
{
    public class CommonBLL
    {
        public List<IRole> GetRole()
        {
            return CommonDAL.GetRole();
        }

        #region Location

        public List<ILocation> GetLocationList()
        {
            return CommonDAL.GetLocationList();
        }

        public ILocation GetLocation(int locId)
        {
            return CommonDAL.GetLocation(locId);
        }

        public void SaveLocation(ILocation loc, int modifiedBy)
        {
            CommonDAL.SaveLocation(loc, modifiedBy);
        }

        #endregion
    }
}
