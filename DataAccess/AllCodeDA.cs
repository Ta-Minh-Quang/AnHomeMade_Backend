using CommonLib;
using DataAccess.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AllCodeDA
    {
        
        public static DataSet AllcodeFindAll()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonData.gConnectionString, CommandType.StoredProcedure, "pkgAllcode_proc_FindAll");
            }
            catch (Exception ex)
            {
                return new DataSet ();
            }
        }
    }
}
