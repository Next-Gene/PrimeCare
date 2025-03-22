using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
namespace eCommerceApp.Application.Services.Interfaces.Authentucation
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUser(CreateUser user);
        Task<LoginResponse> LoginUser(LoginUser user);
        Task<LoginResponse> ReviveTokenUser(string refreshToken);
    }
}
