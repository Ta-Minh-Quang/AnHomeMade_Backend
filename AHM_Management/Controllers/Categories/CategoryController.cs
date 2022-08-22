using DataAccess;
using Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace AHM_Service.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet]
        //[Authorize]
        [Route("api/categories/search")]
        public IActionResult Search(string keySearch, int startRow, int endRow, string orderBy = "")
        {
            try
            {
                //keySearch = "name|"
                decimal totalRecord = 0;
                List<CategoryInfo> lstData = new List<CategoryInfo>();
                DataSet ds = CategoriesDA.Search(keySearch, startRow, endRow, orderBy, ref totalRecord);
                lstData = CBO<CategoryInfo>.FillCollectionFromDataSet(ds);
                return Json(new { totalRows = totalRecord, jsonData = JsonSerializer.Serialize(lstData) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { totalRows = "-1", jsonData = JsonSerializer.Serialize(new List<CategoryInfo>()) });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/categories/insert")]
        public IActionResult Insert([FromBody] CategoryInfo info)
        {
            try
            {
                decimal result = CategoriesDA.Insert(info);
                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpPut]
        [Authorize]
        [Route("api/categories/update")]
        public IActionResult Update([FromBody] CategoryInfo info)
        {
            try
            {
                decimal result = CategoriesDA.Update(info);
                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("api/categories/delete")]
        public IActionResult Delete(decimal id, string modifiedBy)
        {
            try
            {
                decimal result = CategoriesDA.Delete(id, modifiedBy);
                return Json(new { success = result.ToString() });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = "-1" });
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("api/categories/get-by-id")]
        public IActionResult GetDetail(decimal id)
        {
            try
            {
                CategoryInfo cat = new CategoryInfo();
                DataSet ds = CategoriesDA.GetById(id);
                cat = CBO<CategoryInfo>.FillObjectFromDataSet(ds);

                return Json(new { totalRows = 1, jsonData = JsonSerializer.Serialize(cat) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { totalRows = 0, jsonData = JsonSerializer.Serialize(new CategoryInfo()) });
            }
        }
    }
}
