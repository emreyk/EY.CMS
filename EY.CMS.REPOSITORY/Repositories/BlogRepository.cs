using Ey.Cms.CORE.Models;
using EY.CMS.CORE.Repositories;
using Microsoft.EntityFrameworkCore;


namespace EY.CMS.REPOSITORY.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRespository
    {
        public BlogRepository(EyCmsContext context) : base(context)
        {
        }

        public async Task<Blog> GetBlogById(int id)
        {
            return await _context.Blogs.Include(x => x.Blog_Category).Where(x=>x.Id==id).SingleOrDefaultAsync();
        }

        public async Task<List<Blog>> GetBlogsWithCategory()
        {
            return await _context.Blogs.Include(x => x.Blog_Category).ToListAsync();
        }
    }
}
