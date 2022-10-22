
namespace EY.CMS.CORE.DTOs
{
    public  class BlogWithCategoryDto
    {
        public BlogCategoryDto Blog_Category { get; set; }
        public int Id { get; set; }
        public int Blog_CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string SeoUrl { get; set; }
    }
}
