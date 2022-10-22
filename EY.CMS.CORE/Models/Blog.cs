using EY.CMS.CORE.Models;
namespace Ey.Cms.CORE.Models
{
    public class Blog : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string SeoUrl { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int Blog_CategoryId { get; set; }
        public Blog_Category Blog_Category { get; set; }

    }
}
