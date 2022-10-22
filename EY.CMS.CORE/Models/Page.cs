using EY.CMS.CORE.Models;
namespace Ey.Cms.CORE.Models
{
    public class Page:BaseEntity
    {
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string NameRU { get; set; }
        public string SeoUrl { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Text { get; set; }
        public string TextEN { get; set; }
        public string TextRU { get; set; }
        
    }
}
