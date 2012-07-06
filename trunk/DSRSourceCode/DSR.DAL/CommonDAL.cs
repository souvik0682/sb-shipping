﻿using System;
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

        #region Location

        public static List<IRole> GetRole()
        {
            string strExecution = "[common].[uspGetRole]";
            List<IRole> lstRole = new List<IRole>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IRole role = new RoleEntity(reader);
                    lstRole.Add(role);
                }

                reader.Close();
            }

            return lstRole;
        }

        public static IRole GetRole(int roleId)
        {
            string strExecution = "[common].[uspGetRole]";
            IRole role = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@RoleId", roleId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    role = new RoleEntity(reader);
                }

                reader.Close();
            }

            return role;
        }

        #endregion

        #region Location

        public static List<ILocation> GetLocationList(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetLocation]";
            List<ILocation> lstLoc = new List<ILocation>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchAbbr", 3, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchLocName", 50, searchCriteria.LocName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
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

        public static ILocation GetLocation(int locId, char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetLocation]";
            ILocation loc = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
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

        public static List<IArea> GetAreaList(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetArea]";
            List<IArea> lstArea = new List<IArea>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchAreaName", 50, searchCriteria.AreaName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
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

        public static IArea GetArea(int areaId, char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetArea]";
            IArea area = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@AreaId", areaId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
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

        #region Group Company

        public static List<IGroupCompany> GetGroupCompanyList(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetGroupCompany]";
            List<IGroupCompany> lstGroupCompany = new List<IGroupCompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchGroupName", 50, searchCriteria.GroupName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IGroupCompany groupCompany = new GroupCompanyEntity(reader);
                    lstGroupCompany.Add(groupCompany);
                }

                reader.Close();
            }

            return lstGroupCompany;
        }

        public static IGroupCompany GetGroupCompany(int groupCompanyId, char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetGroupCompany]";
            IGroupCompany groupCompany = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@GroupId", groupCompanyId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    groupCompany = new GroupCompanyEntity(reader);
                }

                reader.Close();
            }

            return groupCompany;
        }

        public static int SaveGroupCompany(IGroupCompany groupCompany, int modifiedBy)
        {
            string strExecution = "[common].[uspSaveGroupCompany]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@GroupId", groupCompany.Id);
                oDq.AddVarcharParam("@GroupName", 50, groupCompany.Name);
                oDq.AddVarcharParam("@Address", 200, groupCompany.Address.Address);
                oDq.AddVarcharParam("@City", 20, groupCompany.Address.City);
                oDq.AddVarcharParam("@Pin", 10, groupCompany.Address.Pin);
                oDq.AddVarcharParam("@Phone", 40, groupCompany.Phone);
                oDq.AddCharParam("@IsActive", 1, groupCompany.IsActive);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }

            return result;
        }

        public static void DeleteGroupCompany(int groupCompanyId, int modifiedBy)
        {
            string strExecution = "[common].[uspDeleteGroupCompany]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@GroupId", groupCompanyId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        #endregion

        #region Customer

        public static List<ICustomer> GetCustomerList(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetCustomer]";
            List<ICustomer> lstCustomer = new List<ICustomer>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchLocAbbr", 3, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchCustName", 60, searchCriteria.CustomerName);
                oDq.AddVarcharParam("@SchGroupName", 50, searchCriteria.GroupName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICustomer customer = new CustomerEntity(reader);
                    lstCustomer.Add(customer);
                }

                reader.Close();
            }

            return lstCustomer;
        }

        public static ICustomer GetCustomer(int customerId, char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetCustomer]";
            ICustomer customer = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CustId", customerId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    customer = new CustomerEntity(reader);
                }

                reader.Close();
            }

            return customer;
        }

        public static int SaveCustomer(ICustomer customer, int modifiedBy)
        {
            string strExecution = "[common].[uspSaveCustomer]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CustId", customer.Id);
                oDq.AddIntegerParam("@GroupId", customer.Group.Id);
                oDq.AddIntegerParam("@LocId", customer.Location.Id);
                oDq.AddIntegerParam("@AreaId", customer.Area.Id);
                oDq.AddIntegerParam("@CustTypeId", customer.CustType.Id);
                oDq.AddCharParam("@CorporateOrLocal", 1, customer.CorporateOrLocal);
                oDq.AddVarcharParam("@CustomerName", 60, customer.Name);
                oDq.AddVarcharParam("@Address", 200, customer.Address.Address);
                oDq.AddVarcharParam("@City", 50, customer.Address.City);
                oDq.AddVarcharParam("@Pin", 10, customer.Address.Pin);
                oDq.AddVarcharParam("@Phone1", 50, customer.Phone1);
                oDq.AddVarcharParam("@Phone2", 50, customer.Phone2);
                oDq.AddVarcharParam("@ContactPerson1", 50, customer.ContactPerson1.Name);
                oDq.AddVarcharParam("@ContactDesignation1", 50, customer.ContactPerson1.Designation);
                oDq.AddVarcharParam("@ContactMobile1", 15, customer.ContactPerson1.Mobile);
                oDq.AddVarcharParam("@ContactPerson2", 50, customer.ContactPerson2.Name);
                oDq.AddVarcharParam("@ContactDesignation2", 50, customer.ContactPerson2.Designation);
                oDq.AddVarcharParam("@ContactMobile2", 15, customer.ContactPerson2.Mobile);
                oDq.AddVarcharParam("@CustomerProfile", 500, customer.CustomerProfile);
                oDq.AddVarcharParam("@PAN", 10, customer.PAN);
                oDq.AddVarcharParam("@TAN", 15, customer.TAN);
                oDq.AddVarcharParam("@BIN", 15, customer.BIN);
                oDq.AddVarcharParam("@IEC", 15, customer.IEC);
                //oDq.AddIntegerParam("@SalesExecutive", customer.SalesExecutive);
                oDq.AddCharParam("@IsActive", 1, customer.IsActive);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static void DeleteCustomer(int customerId, int modifiedBy)
        {
            string strExecution = "[common].[uspDeleteCustomer]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CustId", customerId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        #endregion
    }
}
