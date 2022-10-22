using EY.CMS.CORE.DTOs;

namespace EY.CMS.WEB.Models
{
    public class HomeModel
    {
        public List<SliderDto> Sliders { get; set; }
        public List<ProductDto> Product { get; set; }
        public List<ProductDto> AllProduct { get; set; }
    }
}
