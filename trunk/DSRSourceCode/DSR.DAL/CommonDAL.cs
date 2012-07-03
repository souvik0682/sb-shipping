using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DSR.Common;
using DSR.DAL.DbManager;
using DSR.Entity;

namespace DSR.DAL
{
    public sealed class CommonDAL
    {
        private CommonDAL()
        {
        }

        public static List<IRole> GetRole()
        {
            string strExecution = "[common].[uspGetRole]";
            List<IRole> lstRole = new List<IRole>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IRole role = new RoleEntity();
                    role.Id = Convert.ToInt32(reader["pk_RoleID"]);
                    role.Name = Convert.ToString(reader["RoleName"]);
                    role.SalesRole = Convert.ToChar(reader["SalesRole"]);
                    lstRole.Add(role);
                }

                reader.Close();
            }

            return lstRole;
        }

        #region Location

        public static List<ILocation> GetLocationList(char isActiveOnly)
        {
            string strExecution = "[common].[uspGetLocation]";
            List<ILocation> lstLoc = new List<ILocation>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ILocation loc = new LocationEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        public static ILocation GetLocation(int locId, char isActiveOnly)
        {
            string strExecution = "[common].[uspGetLocation]";
            ILocation loc = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    loc = new LocationEntity(reader);
                }

                reader.Close();
            }

            return loc;
        }

        public static int SaveLocation(ILocation loc, int modifiedBy)
        {
            string strExecution = "[common].[uspSaveLocation]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", loc.Id);
                oDq.AddVarcharParam("@LocName", 50, loc.Name);
                oDq.AddVarcharParam("@LocAddress", 200, loc.LocAddress.Address);
                oDq.AddVarcharParam("@LocCity", 20, loc.LocAddress.City);
                oDq.AddVarcharParam("@LocPin", 10, loc.LocAddress.Pin);
                oDq.AddVarcharParam("@LocAbbr", 3, loc.Abbreviation);
                oDq.AddVarcharParam("@LocPhone", 30, loc.Phone);
                oDq.AddIntegerParam("@ManagerId", loc.ManagerId);
                oDq.AddCharParam("@IsActive", 1, loc.IsActive);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }

            return result;
        }

        public static void DeleteLocation(int locId, int modifiedBy)
        {
            string strExecution = "[common].[uspDeleteLocation]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        #endregion

        #region Area

        public static List<IArea> GetAreaList(char isActiveOnly)
        {
            string strExecution = "[common].[uspGetArea]";
            List<IArea> lstArea = new List<IArea>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IArea area = new AreaEntity(reader);
                    lstArea.Add(area);
                }

                reader.Close();
            }

            return lstArea;
        }

        public static IArea GetArea(int areaId, char isActiveOnly)
        {
            string strExecution = "[common].[uspGetArea]";
            IArea area = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AreaId", areaId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    area = new AreaEntity(reader);
                }

                reader.Close();
            }

            return area;
        }

        public static int SaveArea(IArea area, int modifiedBy)
        {
            string strExecution = "[common].[uspSaveArea]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AreaId", area.Id);
                oDq.AddVarcharParam("@AreaName", 50, area.Name);
                oDq.AddIntegerParam("@LocId", area.Location.Id);
                oDq.AddCharParam("@IsActive", 1, area.IsActive);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static void DeleteArea(int areaId, int modifiedBy)
        {
            string strExecution = "[common].[uspDeleteArea]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AreaId", areaId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        #endregion
    }
}
