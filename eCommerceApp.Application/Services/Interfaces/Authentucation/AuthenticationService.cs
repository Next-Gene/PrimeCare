using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Domain.Interfaces.Authentication;

namespace eCommerceApp.Application.Services.Interfaces.Authentucation;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenManagement _tokenManagement;
    private readonly IUserManagement _userManagement;
    private readonly IRoleManagement _roleManagement;
    private readonly IAppLogger<AuthenticationService> _logger;
    private readonly IMapper _mapper;
    public AuthenticationService
        (ITokenManagement tokenManagement,
        IUserManagement userManagement,
        IRoleManagement roleManagement,
        IAppLogger<AuthenticationService> logger,
        IMapper mapper)
    {
        _tokenManagement = tokenManagement;
        _userManagement = userManagement;
        _roleManagement = roleManagement;
        _logger = logger;
        _mapper = mapper;
    }
    public Task<ServiceResponse> CreateUser(CreateUser user)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponse> LoginUser(LoginUser user)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponse> ReviveTokenUser(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
