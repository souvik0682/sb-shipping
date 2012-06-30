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

        public static List<ILocation> GetLocationList()
        {
            string strExecution = "[common].[uspGetLocation]";
            List<ILocation> lstLoc = new List<ILocation>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
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

        public static ILocation GetLocation(int locId)
        {
            string strExecution = "[common].[uspGetLocation]";
            ILocation loc = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", locId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    loc = new LocationEntity(reader);
                }

                reader.Close();
            }

            return loc;
        }

        public static void SaveLocation(ILocation loc, int modifiedBy)
        {
            string strExecution = "[common].[uspSaveLocation]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", loc.Id);
                oDq.AddVarcharParam("@LocName",50, loc.Name);
                oDq.AddVarcharParam("@LocAddress", 200, loc.LocAddress.Address);
                oDq.AddVarcharParam("@LocCity", 20, loc.LocAddress.City);
                oDq.AddVarcharParam("@LocPin", 10, loc.LocAddress.Pin);
                oDq.AddVarcharParam("@LocAbbr", 3, loc.Abbreviation);
                oDq.AddVarcharParam("@LocPhone", 30, loc.Phone);
                oDq.AddIntegerParam("@ManagerId", loc.ManagerId);
                oDq.AddCharParam("@IsActive", 1, loc.IsActive);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        #endregion
    }
}
