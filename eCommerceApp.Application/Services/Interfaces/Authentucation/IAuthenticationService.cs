using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;

namespace eCommerceApp.Application.Services.Interfaces.Authentucation;

public interface IAuthenticationService
{
    Task<ServiceResponse> CreateUser(CreateUserDto user);
    Task<LoginResponse> LoginUser(LoginUserDto user);
    Task<LoginResponse> ReviveTokenUser(string refreshToken);
}
