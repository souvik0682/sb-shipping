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

        #region Area

        private void SetDefaultSearchCriteriaForArea(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IArea> GetAllAreaList(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetAreaList('N', searchCriteria);
        }

        public List<IArea> GetActiveAreaList()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForArea(searchCriteria);
            return CommonDAL.GetAreaList('Y', searchCriteria);
        }

        public IArea GetArea(int areaId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForArea(searchCriteria);
            return CommonDAL.GetArea(areaId, 'N', searchCriteria);
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

        #region Group Company

        private void SetDefaultSearchCriteriaForGroupCompany(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IGroupCompany> GetAllGroupCompanyList(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetGroupCompanyList('N', searchCriteria);
        }

        public List<IGroupCompany> GetActiveGroupCompanyList()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForGroupCompany(searchCriteria);
            return CommonDAL.GetGroupCompanyList('Y', searchCriteria);
        }

        public IGroupCompany GetGroupCompany(int groupCompanyId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetGroupCompany(groupCompanyId, 'N', searchCriteria);
        }

        public string SaveGroupCompany(IGroupCompany groupCompany, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CommonDAL.SaveGroupCompany(groupCompany, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00014");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteGroupCompany(int groupCompanyId, int modifiedBy)
        {
            CommonDAL.DeleteGroupCompany(groupCompanyId, modifiedBy);
        }

        #endregion
    }
}
