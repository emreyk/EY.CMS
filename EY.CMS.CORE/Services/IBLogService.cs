using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;

namespace EY.CMS.CORE.Services
{
    public interface IBlogService : IService<Blog>
    {
        Task<List<BlogWithCategoryDto>> GetBlogsWithCategory();
        Task<BlogWithCategoryDto> GetBlogById(int id);
    }
}
