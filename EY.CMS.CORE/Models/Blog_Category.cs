using EY.CMS.CORE.Models;

namespace Ey.Cms.CORE.Models
{
    public  class Blog_Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Blog> Blog { get; set; }
    }
}
