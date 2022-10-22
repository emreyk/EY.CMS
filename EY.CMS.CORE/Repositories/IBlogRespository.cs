using Ey.Cms.CORE.Models;

namespace EY.CMS.CORE.Repositories
{
    public interface IBlogRespository : IGenericRepository<Blog>
    {
        Task<List<Blog>> GetBlogsWithCategory();
        Task<Blog> GetBlogById(int id);
    }
}
