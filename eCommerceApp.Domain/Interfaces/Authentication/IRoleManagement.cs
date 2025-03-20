using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    // <summary>
    // / Provides an interface for managing user roles.
    public interface IRoleManagement
    {
        Task<string?> GetUserRole(string userEmail);
        Task<bool> AddUserToRole(AppUser user, string roleName);
    }
}
