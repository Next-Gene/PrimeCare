using System.Security.Claims;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    // / <summary>
    // / Provides an interface for managing user tokens.
    public interface ITokenManagement
    {
        string GetRefreshToken();
        List<Claim> GetUserClaims(string email);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<string> GetUserIdByRefreshToken(string refreshToken);
        Task<int> AddRefreshToken(string userId, string refreshToken);
        Task<int> UpdateRefreshToken(string userId, string refreshToken);
        string GenerateToken(List<Claim> claims);
    }
}
