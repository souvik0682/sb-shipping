using System;
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

        private void SetDefaultSearchCriteriaForLocation(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<ILocation> GetAllLocationList(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetLocationList('N', searchCriteria);
        }

        public List<ILocation> GetActiveLocationList()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetLocationList('Y', searchCriteria);
        }

        public ILocation GetLocation(int locId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetLocation(locId, 'N', searchCriteria);
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
