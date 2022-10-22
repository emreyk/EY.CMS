using EY.CMS.CORE.Models;
namespace EY.CMS.CORE.DTOs
{
    public class ServiceDto : BaseEntity
    {
        public string Name { get; set; }
        public string SeoUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortText { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}
