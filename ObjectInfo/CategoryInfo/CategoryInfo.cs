using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectInfo
{
    public class CategoryInfo
    {
        public decimal STT { get; set; }
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public decimal Deleted { get; set; }
    }
}
