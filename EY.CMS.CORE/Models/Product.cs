using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EY.CMS.CORE.Models
{
    public class Product:BaseEntity
    {
        public int ProductParentId { get; set; }
        public string ProductName { get; set; }
        public string ProductNameEN { get; set; }
        public string ProductNameRU { get; set; }
        public string Text { get; set; }
        public string TextEN { get; set; }
        public string TextRU { get; set; }
        public string Image { get; set; }
        public string BannerImage { get; set; }
        public string SeoUrl { get; set; }
    }
}
