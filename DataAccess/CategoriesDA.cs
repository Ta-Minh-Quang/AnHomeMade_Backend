using CommonLib;
using DataAccess.Helper;
using Lib;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class CategoriesDA
    {
        public static DataSet Search(string keySearch, int startRow, int endRow, string orderBy, ref decimal totalRecord)
        {
            try
            {
                var lstParam = new SqlParameter[5];
                lstParam[0] = new SqlParameter("@p_key_search", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = keySearch
                };
                lstParam[1] = new SqlParameter("@p_startrow", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = startRow
                };
                lstParam[2] = new SqlParameter("@p_endrow", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = endRow
                };
                lstParam[3] = new SqlParameter("@p_orderby", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = orderBy
                };
                lstParam[4] = new SqlParameter("@p_total_record", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgCategories_proc_Search", lstParam);
                totalRecord = Convert.ToDecimal(lstParam[4].Value.ToString());
                return dt;

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }


        public static DataSet GetById(decimal id)
        {
            try
            {
                DataSet dt = new DataSet();
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                dt = SqlHelper.ExecuteDataset(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgCategories_proc_GetById", lstParam);

                return dt;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public static decimal Insert(CategoryInfo info)
        {

            try
            {
                #region create parameters
                var lstParam = new SqlParameter[4];
                lstParam[0] = new SqlParameter("@p_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Name
                };
                lstParam[1] = new SqlParameter("@p_note", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Note
                };
                lstParam[2] = new SqlParameter("@p_created_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Created_By
                };
                lstParam[3] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                #endregion

                SqlHelper.ExecuteNonQuery(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgCategories_proc_Insert", lstParam);

                return Convert.ToDecimal(lstParam[3].Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1101;
            }

        }

        public static decimal Update(CategoryInfo info)
        {

            try
            {
                #region create parameters
                var lstParam = new SqlParameter[5];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Id
                };
                lstParam[1] = new SqlParameter("@p_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Name
                };
                lstParam[2] = new SqlParameter("@p_note", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Note
                };
                lstParam[3] = new SqlParameter("@p_modified_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Created_By
                };
                lstParam[4] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                #endregion
                SqlHelper.ExecuteNonQuery(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgCategories_proc_Update", lstParam);

                return Convert.ToDecimal(lstParam[4].Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1101;
            }

        }

        public static decimal Delete(decimal id, string modified_by)
        {
            try
            {
                var lstParam = new SqlParameter[3];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };
                lstParam[1] = new SqlParameter("@p_modified_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = modified_by
                };
                lstParam[2] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                };

                SqlHelper.ExecuteNonQuery(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgCategories_proc_Delete", lstParam);
                return Convert.ToDecimal(lstParam[2].Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1101;
            }
        }
    }
}
