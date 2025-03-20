using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public String Fullname { get; set; } = String.Empty;
    }
}
// AppUser