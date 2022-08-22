using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectInfo
{
    public class ProductsInfo
    {
        public decimal STT { get; set; }
        public decimal Id { get; set; }
        public decimal Item_Id { get; set; }
        public decimal Shop_Id { get; set; }
        public string Name { get; set; }
        public float Discount { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public decimal Sold { get; set; }
        public string Description { get; set; }
        public float Rating_Star { get; set; }
        public string Images { get; set; }
        public string Image { get; set; }
        public string Url_Video { get; set; }
        public decimal Category_Id { get; set; }
        public string Url_Item { get; set; }
        public string Weight { get; set; }
        public string Brand { get; set; }
        public string Origin { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public decimal Deleted { get; set; }

        //tên danh mục
        public string Caterogy_Name { get; set; }
    }
}
