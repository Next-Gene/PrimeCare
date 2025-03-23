using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentucation;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Domain.Interfaces.Authentication;
using FluentValidation;

namespace eCommerceApp.Application.Services.Implementations.Authentucation;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenManagement _tokenManagement;
    private readonly IUserManagement _userManagement;
    private readonly IRoleManagement _roleManagement;
    private readonly IAppLogger<AuthenticationService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IValidator<LoginUserDto> _loginUserValidator;

    public AuthenticationService(ITokenManagement tokenManagement,
        IUserManagement userManagement, IRoleManagement roleManagement,
        IAppLogger<AuthenticationService> logger, IMapper mapper,
        IValidator<CreateUserDto> createUserValidator,
        IValidator<LoginUserDto> loginUserValidator)
    {
        _tokenManagement = tokenManagement;
        _userManagement = userManagement;
        _roleManagement = roleManagement;
        _logger = logger;
        _mapper = mapper;
        _createUserValidator = createUserValidator;
        _loginUserValidator = loginUserValidator;
    }

    public async Task<ServiceResponse> CreateUser(CreateUserDto user)
    {
        var _validation = await _createUserValidator.ValidateAsync(user);

        if (!_validation.IsValid)
        {
            var errors = _validation.Errors
                .Select(e => e.ErrorMessage).ToList();
        }
    }

    public Task<LoginResponse> LoginUser(LoginUserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponse> ReviveTokenUser(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
