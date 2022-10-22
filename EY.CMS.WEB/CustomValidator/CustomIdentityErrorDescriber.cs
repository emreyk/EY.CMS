using Microsoft.AspNetCore.Identity;

namespace EY.CMS.WEB.CustomValidator
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string UserName)
        {
            return new IdentityError()
            {
                Code = "InvalidUserName",
                Description = $" Bu {UserName} kullanıcı adı geçersizdir"
            };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = $" Bu {email} kullanılmaktadır"
            };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $" Bu {userName} kullanılmaktadır"
            };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordToShort",
                Description = $" Şifreniz en az {length} karakter olmalıdır"
            };
        }
    }
}
