using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories.Authentication;

/// <summary>
/// Provides methods for managing tokens.
/// </summary>
public class TokenManagement : ITokenManagement
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenManagement"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="configuration">The configuration settings.</param>
    public TokenManagement(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    /// <summary> 
    /// Adds a new refresh token for a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="refreshToken">The refresh token to add.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an integer indicating the result of the operation.</returns>
    public async Task<int> AddRefreshToken(string userId, string refreshToken)
    {
        _dbContext.RefreshToken.Add(new RefreshToken
        {
            UserId = userId,
            Token = refreshToken
        });

        return await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Generates a new token based on the provided claims.
    /// </summary>
    /// <param name="claims">The claims to include in the token.</param>
    /// <returns>A new token as a string.</returns>
    public string GenerateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration["JWT:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issure"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Generates a new refresh token.
    /// </summary>
    /// <returns>A new refresh token as a string.</returns>
    public string GetRefreshToken()
    {
        const int byteSize = 64;
        byte[] randomBytes = new byte[byteSize];

        // rng is => randomNumberGenerator
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        string token = Convert.ToBase64String(randomBytes);
        return WebUtility.UrlEncode(token);
    }

    /// <summary>
    /// Retrieves user claims from a token.
    /// </summary>
    /// <param name="token">The token to extract claims from.</param>
    /// <returns>A list of claims associated with the token.</returns>
    public List<Claim> GetUserClaimsFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);

        if (jwtToken != null)
            return jwtToken.Claims.ToList();
        else
            return [];
    }

    /// <summary>
    /// Retrieves the user ID associated with a given refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the user ID as a string.</returns>
    public async Task<string> GetUserIdByRefreshToken(string refreshToken)
        => (await _dbContext.RefreshToken
        .FirstOrDefaultAsync(_ => _.Token == refreshToken))!.UserId;

    /// <summary>
    /// Updates the refresh token for a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="refreshToken">The new refresh token.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an integer indicating the result of the operation.</returns>
    public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
    {
        var user = await _dbContext.RefreshToken
            .FirstOrDefaultAsync(_ => _.Token == refreshToken);

        if (user == null) return -1;

        user.Token = refreshToken;

        return await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Validates a given refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to validate.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a boolean value indicating whether the refresh token is valid.</returns>
    public async Task<bool> ValidateRefreshToken(string refreshToken)
    {
        var user = await _dbContext.RefreshToken
            .FirstOrDefaultAsync(_ => _.Token == refreshToken);

        return user != null;
    }
}
