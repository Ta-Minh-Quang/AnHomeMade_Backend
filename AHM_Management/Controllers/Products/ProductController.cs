using DataAccess;
using Leaf.xNet;
using Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;

namespace AHM_Service.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        [HttpGet]
        //[Authorize]
        [Route("api/products/search")]
        public IActionResult Search(string keySearch, int startRow, int endRow, string orderBy = "")
        {
            try
            {
                //keySearch = "name|brand|category_id|status|min_price|max_price|"
                decimal totalRecord = 0;
                List<ProductsInfo> lstData = new List<ProductsInfo>();
                DataSet ds = ProductsDA.Search(keySearch, startRow, endRow, orderBy, ref totalRecord);
                lstData = CBO<ProductsInfo>.FillCollectionFromDataSet(ds);
                return Json(new { totalRows = totalRecord, jsonData = JsonSerializer.Serialize(lstData) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { totalRows = "-1", jsonData = JsonSerializer.Serialize(new List<ProductsInfo>()) });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/products/insert")]
        public IActionResult Insert([FromBody] ProductsInfo info)
        {
            try
            {
                decimal result = ProductsDA.Insert(info);
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
        [Route("api/products/update")]
        public IActionResult Update([FromBody] ProductsInfo info)
        {
            try
            {
                decimal result = ProductsDA.Update(info);
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
        [Route("api/products/delete")]
        public IActionResult Delete(decimal id, string modifiedBy)
        {
            try
            {
                decimal result = ProductsDA.Delete(id, modifiedBy);
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
        [Route("api/products/get-by-id")]
        public IActionResult GetDetail(decimal id)
        {
            try
            {
                ProductsInfo product = new ProductsInfo();
                DataSet ds = ProductsDA.GetById(id);
                product = CBO<ProductsInfo>.FillObjectFromDataSet(ds);

                return Json(new { totalRows = 1, jsonData = JsonSerializer.Serialize(product) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { totalRows = 0, jsonData = JsonSerializer.Serialize(new ProductsInfo()) });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/products/get-by-itemid-shopid")]
        public IActionResult GetDetailByLink(decimal idItem, decimal idShop)
        {
            try
            {
                ProductsInfo _productInfo = new ProductsInfo();
                JObject data = new JObject();
                var httpRequest = new HttpRequest();
                httpRequest.AddHeader(HttpHeader.Accept, "application/json");
                httpRequest.AddHeader(HttpHeader.ContentType, "application/json");
                httpRequest.AddHeader(HttpHeader.IfNoneMatch, "*");

                var resp = httpRequest.Get($"https://shopee.vn/api/v4/item/get?itemid={idItem}&shopid={idShop}");

                if (resp != null && resp.IsOK)
                {
                    JObject responeObject = JObject.Parse(resp.ToString());
                    data = JObject.Parse(responeObject["data"].ToString());
                    _productInfo.Item_Id = Convert.ToDecimal(data["itemid"].ToString());
                    _productInfo.Shop_Id = Convert.ToDecimal(data["shopid"].ToString());
                    _productInfo.Name = data["name"].ToString();
                    _productInfo.Discount = (float)(Convert.ToDecimal(data["show_discount"].ToString()) / 100);
                    _productInfo.Price = Convert.ToDecimal(data["price_before_discount"].ToString())/100000;
                    _productInfo.Stock = Convert.ToDecimal(data["stock"].ToString());
                    _productInfo.Sold = Convert.ToDecimal(data["historical_sold"].ToString());
                    _productInfo.Description = data["description"].ToString();
                    _productInfo.Images = string.Join(",", (JArray)data["images"]);
                    _productInfo.Image = data["image"].ToString() == null ? "" : data["image"].ToString();

                    try
                    {
                        JArray video_info_list = ((JArray)data["video_info_list"]);
                        string url_video = video_info_list[0]["default_format"]["url"].ToString();
                        _productInfo.Url_Video = string.IsNullOrEmpty(url_video) ? "" : url_video;
                    }
                    catch (Exception ex)
                    {
                        _productInfo.Url_Video = null;
                    }
                }

                return Json(new { success = JsonSerializer.Serialize(_productInfo) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return Json(new { success = JsonSerializer.Serialize(new ProductsInfo())});
            }
        }
    }
}
