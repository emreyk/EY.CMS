using EY.CMS.CORE.Models;

namespace EY.CMS.CORE.DTOs
{
    public class AppUserDto:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
