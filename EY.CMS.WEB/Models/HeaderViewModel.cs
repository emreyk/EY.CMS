using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;

namespace EY.CMS.WEB.Models
{
    public class HeaderViewModel
    {
        public SettingDto Setting { get; set; }
        public List<PageDto> Pages { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
