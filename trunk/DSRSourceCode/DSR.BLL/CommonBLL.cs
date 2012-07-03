﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSR.DAL;
using DSR.Common;
using DSR.Entity;
using DSR.Utilities.ResourceManager;

namespace DSR.BLL
{
    public class CommonBLL
    {
        public List<IRole> GetRole()
        {
            return CommonDAL.GetRole();
        }

        #region Location

        public List<ILocation> GetAllLocationList()
        {
            return CommonDAL.GetLocationList('N');
        }

        public List<ILocation> GetActiveLocationList()
        {
            return CommonDAL.GetLocationList('Y');
        }

        public ILocation GetLocation(int locId)
        {
            return CommonDAL.GetLocation(locId, 'N');
        }

        public string SaveLocation(ILocation loc, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CommonDAL.SaveLocation(loc, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00011");
                    break;
                case 2:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00012");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteLocation(int locId, int modifiedBy)
        {
            CommonDAL.DeleteLocation(locId, modifiedBy);
        }

        #endregion

        #region Location

        public List<IArea> GetAllAreaList()
        {
            return CommonDAL.GetAreaList('N');
        }

        public List<IArea> GetActiveAreaList()
        {
            return CommonDAL.GetAreaList('Y');
        }

        public IArea GetArea(int areaId)
        {
            return CommonDAL.GetArea(areaId, 'N');
        }

        public string SaveArea(IArea area, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CommonDAL.SaveArea(area, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00013");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteArea(int areaId, int modifiedBy)
        {
            CommonDAL.DeleteArea(areaId, modifiedBy);
        }

        #endregion
    }
}
