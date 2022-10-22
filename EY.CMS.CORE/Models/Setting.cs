using EY.CMS.CORE.Models;

namespace Ey.Cms.CORE.Models
{
    public class Setting:BaseEntity
    {
        public string Keyword { get; set; }
        public string Title { get; set; }
        public string Descripton { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Logo { get; set; }
    }
}
