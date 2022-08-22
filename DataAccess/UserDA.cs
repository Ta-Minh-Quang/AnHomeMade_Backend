using CommonLib;
using DataAccess.Helper;
using Lib;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDA
    {
        public UserInfo AuthenUser(string username, string password)
        {
            try
            {
                var lstParam = new SqlParameter[3];
                lstParam[0] = new SqlParameter("@p_user_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = username
                };
                lstParam[1] = new SqlParameter("@p_password", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = password
                };
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgUser_proc_Login", lstParam);
                UserInfo result = CBO<UserInfo>.FillObjectFromDataSet(ds);
                return result;
            }
            catch (Exception e)
            {
                return new UserInfo();
            }
        }
    }
}
