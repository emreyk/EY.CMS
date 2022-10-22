using EY.CMS.CORE.Models;

namespace EY.CMS.CORE.DTOs
{
    public class SliderDto: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
