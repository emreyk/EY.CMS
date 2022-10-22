using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.IUnitOfWork;
using EY.CMS.CORE.Repositories;
using EY.CMS.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EY.CMS.SERVICE.Services
{
    public class BlogService : Service<Blog>, IBlogService
    {
        private readonly IBlogRespository _blogService;
        private readonly IMapper _mapper;

        public BlogService(IGenericRepository<Blog> repository, IMapper mapper, IBlogRespository blogService, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        public async Task<List<BlogWithCategoryDto>> GetBlogsWithCategory()
        {
            var blogs = await _blogService.GetBlogsWithCategory();
            return _mapper.Map<List<BlogWithCategoryDto>>(blogs);
        }

        public async Task<BlogWithCategoryDto> GetBlogById(int id)
        {
            var blog = await _blogService.GetBlogById(id);
            return _mapper.Map<BlogWithCategoryDto>(blog);
        }

    }
}
