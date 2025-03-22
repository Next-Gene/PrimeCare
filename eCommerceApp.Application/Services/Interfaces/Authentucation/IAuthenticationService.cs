using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
namespace eCommerceApp.Application.Services.Interfaces.Authentucation;

public interface IAuthenticationService
{
    Task<ServiceResponse> CreateUser(CreateUser user);
    Task<LoginResponse> LoginUser(LoginUser user);
    Task<LoginResponse> ReviveTokenUser(string refreshToken);
}
