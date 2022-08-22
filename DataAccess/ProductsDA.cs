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
    public class ProductsDA
    {
        public static DataSet Search(string keySearch, int startRow, int endRow, string orderBy, ref decimal totalRecord)
        {
            try
            {
                if (keySearch != null)
                {

                    var lstKeysearch = keySearch.Split('|');

                    var lstParam = new SqlParameter[6];
                    lstParam[0] = new SqlParameter("@p_key_search", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = keySearch
                    };
                    lstParam[1] = new SqlParameter("@p_name_search", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = lstKeysearch[0]
                    };
                    lstParam[2] = new SqlParameter("@p_startrow", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = startRow
                    };
                    lstParam[3] = new SqlParameter("@p_endrow", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = endRow
                    };
                    lstParam[4] = new SqlParameter("@p_orderby", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = orderBy
                    };
                    lstParam[5] = new SqlParameter("@p_total_record", SqlDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output,
                        Value = -1
                    };

                    var dt = SqlHelper.ExecuteDataset(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgProducts_proc_Search", lstParam);
                    totalRecord = Convert.ToDecimal(lstParam[5].Value.ToString());
                    return dt;
                }
                else
                {
                    return null;
                }

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
                var lstParam = new SqlParameter[2];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                dt = SqlHelper.ExecuteDataset(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgProducts_proc_GetById", lstParam);  

                return dt;
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        public static decimal Insert(ProductsInfo info)
        {

            try
            {
                #region create parameters
                var lstParam = new SqlParameter[19];
                lstParam[0] = new SqlParameter("@p_item_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Item_Id
                };
                lstParam[1] = new SqlParameter("@p_shop_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Shop_Id
                };
                lstParam[2] = new SqlParameter("@p_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Name
                };
                lstParam[3] = new SqlParameter("@p_discount", SqlDbType.Float)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Discount
                };
                lstParam[4] = new SqlParameter("@p_price", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Price
                };
                lstParam[5] = new SqlParameter("@p_stock", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Stock
                };
                lstParam[6] = new SqlParameter("@p_sold", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Sold
                };
                lstParam[7] = new SqlParameter("@p_description", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = string.IsNullOrEmpty(info.Description) ? "" : info.Description,
                    IsNullable = true
                };
                lstParam[8] = new SqlParameter("@p_rating_star", SqlDbType.Float)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Rating_Star
                };
                lstParam[9] = new SqlParameter("@p_images", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Images
                };
                lstParam[10] = new SqlParameter("@p_image", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Image
                };
                lstParam[11] = new SqlParameter("@p_url_video", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = string.IsNullOrEmpty(info.Url_Video) ? "" : info.Url_Video,
                    IsNullable = true
                };
                lstParam[12] = new SqlParameter("@p_category_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Category_Id
                };
                lstParam[13] = new SqlParameter("@p_url_item", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Url_Item
                };
                lstParam[14] = new SqlParameter("@p_weight", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Weight
                };
                lstParam[15] = new SqlParameter("@p_brand", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Brand
                };
                lstParam[16] = new SqlParameter("@p_origin", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Origin
                };
                lstParam[17] = new SqlParameter("@p_created_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Created_By
                };
                lstParam[18] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                #endregion

                SqlHelper.ExecuteNonQuery(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgProducts_proc_Insert", lstParam);

                return Convert.ToDecimal(lstParam[18].Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1101;
            }

        }

        public static decimal Update(ProductsInfo info)
        {

            try
            {
                #region create parameters
                var lstParam = new SqlParameter[16];
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
                lstParam[2] = new SqlParameter("@p_discount", SqlDbType.Float)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Discount
                };
                lstParam[3] = new SqlParameter("@p_price", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Price
                };
                lstParam[4] = new SqlParameter("@p_stock", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Stock
                };
                lstParam[5] = new SqlParameter("@p_sold", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Sold
                };
                lstParam[6] = new SqlParameter("@p_description", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = string.IsNullOrEmpty(info.Description) ? "" : info.Description,
                    IsNullable = true
                };
                lstParam[7] = new SqlParameter("@p_rating_star", SqlDbType.Float)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Rating_Star
                };
                lstParam[8] = new SqlParameter("@p_images", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Images
                };
                lstParam[9] = new SqlParameter("@p_image", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Image
                };
                lstParam[10] = new SqlParameter("@p_url_video", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = string.IsNullOrEmpty(info.Url_Video) ? "" : info.Url_Video,
                    IsNullable = true
                };
                lstParam[11] = new SqlParameter("@p_weight", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Weight
                };
                lstParam[12] = new SqlParameter("@p_brand", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Brand
                };
                lstParam[13] = new SqlParameter("@p_origin", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Origin
                };
                lstParam[14] = new SqlParameter("@p_modified_by", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = info.Created_By
                };
                lstParam[15] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                #endregion
                SqlHelper.ExecuteNonQuery(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgProducts_proc_Update", lstParam);

                return Convert.ToDecimal(lstParam[15].Value.ToString());
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

                SqlHelper.ExecuteNonQuery(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgProducts_proc_Delete", lstParam);
                return Convert.ToDecimal(lstParam[2].Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return -1102;
            }
        }
    }
}
