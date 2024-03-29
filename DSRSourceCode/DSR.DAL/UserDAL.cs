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
    public sealed class UserDAL
    {
        private UserDAL()
        {
        }

        public static bool ChangePassword(IUser user)
        {
            string strExecution = "[admin].[uspChangePassword]";
            bool result = false;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", user.Id);
                oDq.AddVarcharParam("@OldPwd", 50, user.Password);
                oDq.AddVarcharParam("@NewPwd", 50, user.NewPassword);
                oDq.AddBooleanParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToBoolean(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static void ValidateUser(IUser user)
        {
            string strExecution = "[admin].[uspValidateUser]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@UserName", 10, user.Name);
                oDq.AddVarcharParam("@Password", 50, user.Password);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    user.Id = Convert.ToInt32(reader["UserId"]);
                    user.FirstName = Convert.ToString(reader["FirstName"]);
                    user.LastName = Convert.ToString(reader["LastName"]);
                    user.EmailId = Convert.ToString(reader["emailID"]);
                    user.UserRole.Id = Convert.ToInt32(reader["RoleId"]);

                    if (reader["SalesRole"] != DBNull.Value)
                        user.UserRole.SalesRole = Convert.ToChar(reader["SalesRole"]);

                    user.UserLocation.Id = Convert.ToInt32(reader["LocId"]);

                    if (reader["SalesPersonType"] != DBNull.Value)
                        user.SalesPersonType = Convert.ToChar(reader["SalesPersonType"]);
                }

                reader.Close();
            }
        }

        public static List<IUser> GetUserList(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[admin].[uspGetUser]";
            List<IUser> lstUser = new List<IUser>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchUserName", 10, searchCriteria.UserName);
                oDq.AddVarcharParam("@SchFirstName", 30, searchCriteria.FirstName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IUser user = new UserEntity(reader);
                    lstUser.Add(user);
                }

                reader.Close();
            }

            return lstUser;
        }

        public static IUser GetUser(int userId, char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[admin].[uspGetUser]";
            IUser user = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    user = new UserEntity(reader);
                }

                reader.Close();
            }

            return user;
        }

        public static List<IUser> GetUserByRole(int roleId)
        {
            string strExecution = "[admin].[uspGetUserByRole]";
            List<IUser> lstUser = new List<IUser>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@RoleId", roleId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IUser user = new UserEntity(reader);
                    lstUser.Add(user);
                }

                reader.Close();
            }

            return lstUser;
        }

        public static int SaveUser(IUser user, int modifiedBy)
        {
            string strExecution = "[admin].[uspSaveUser]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", user.Id);
                oDq.AddVarcharParam("@UserName", 10, user.Name);
                oDq.AddVarcharParam("@Pwd", 50, user.Password);
                oDq.AddVarcharParam("@FirstName", 30, user.FirstName);
                oDq.AddVarcharParam("@LastName", 30, user.LastName);
                oDq.AddIntegerParam("@RoleId", user.UserRole.Id);
                oDq.AddIntegerParam("@LocId", user.UserLocation.Id);

                if (user.SalesPersonType != '0')
                    oDq.AddCharParam("@SalesPersonType", 1, user.SalesPersonType);

                oDq.AddVarcharParam("@EmailId", 50, user.EmailId);
                oDq.AddCharParam("@IsActive", 1, user.IsActive);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }

            return result;
        }

        public static void DeleteUser(int userId, int modifiedBy)
        {
            string strExecution = "[admin].[uspDeleteUser]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        public static void ResetPassword(IUser user, int modifiedBy)
        {
            string strExecution = "[admin].[uspResetPassword]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", user.Id);
                oDq.AddVarcharParam("@Pwd", 50, user.Password);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();

            }
        }
    }
}
