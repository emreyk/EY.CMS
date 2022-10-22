using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EY.CMS.CORE.DTOs
{
    public class BlogViewModel
    {
        public List<BlogCategoryDto> BlogCategories { get; set; }
        public BlogWithCategoryDto BlogWithCategory { get; set; }
    }
}
