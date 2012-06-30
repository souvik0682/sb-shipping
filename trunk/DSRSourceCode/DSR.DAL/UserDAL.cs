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
    public sealed class UserDAL
    {
        private UserDAL()
        {
        }

        public static List<IUser> GetAllUser(ISearchCriteria searchCriteria)
        {
            string strExecution = "[admin].[uspGetAllUser]";
            List<IUser> lstUser = new List<IUser>();

            try
            {
                using (DbQuery oDq = new DbQuery(strExecution))
                {
                    if (!ReferenceEquals(searchCriteria, null))
                    {
                        oDq.AddVarcharParam("@SortExpression", 20, searchCriteria.SortExpression);
                        oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                    }

                    DataTableReader reader = oDq.GetTableReader();
                    
                    while (reader.Read())
                    {
                        IUser user = new UserEntity(reader);
                        lstUser.Add(user);
                    }

                    reader.Close();
                }
            }
            catch
            {
                throw;
            }

            return lstUser;
        }
    }
}
