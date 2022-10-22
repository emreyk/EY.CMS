using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;


namespace EY.CMS.SERVICE.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            
            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<Referance, ReferanceDto>().ReverseMap();
            CreateMap<Gallery, GalleryDto>().ReverseMap();
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<Setting, SettingDto>().ReverseMap();
            CreateMap<Blog_Category, BlogCategoryDto>().ReverseMap();
            CreateMap<Blog, BlogWithCategoryDto>().ReverseMap();
            CreateMap<AppRole, AppRoleDto>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
