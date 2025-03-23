using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentucation;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Application.Validations;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using FluentValidation;

namespace eCommerceApp.Application.Services.Implementations.Authentucation;

/// <summary>
/// Provides authentication services for the application.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenManagement _tokenManagement;
    private readonly IUserManagement _userManagement;
    private readonly IRoleManagement _roleManagement;
    private readonly IAppLogger<AuthenticationService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IValidator<LoginUserDto> _loginUserValidator;
    private readonly IValidationService _validationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="tokenManagement">The token management service.</param>
    /// <param name="userManagement">The user management service.</param>
    /// <param name="roleManagement">The role management service.</param>
    /// <param name="logger">The logger service.</param>
    /// <param name="mapper">The mapper service.</param>
    /// <param name="createUserValidator">The validator for creating users.</param>
    /// <param name="loginUserValidator">The validator for logging in users.</param>
    /// <param name="validationService">The validation service.</param>
    public AuthenticationService(ITokenManagement tokenManagement,
        IUserManagement userManagement, IRoleManagement roleManagement,
        IAppLogger<AuthenticationService> logger, IMapper mapper,
        IValidator<CreateUserDto> createUserValidator,
        IValidator<LoginUserDto> loginUserValidator,
        IValidationService validationService)
    {
        _tokenManagement = tokenManagement;
        _userManagement = userManagement;
        _roleManagement = roleManagement;
        _logger = logger;
        _mapper = mapper;
        _createUserValidator = createUserValidator;
        _loginUserValidator = loginUserValidator;
        _validationService = validationService;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
    public async Task<ServiceResponse> CreateUser(CreateUserDto user)
    {
        var _validationResult = await _validationService
            .ValidateAsync(user, _createUserValidator);
        if (!_validationResult.Success) return _validationResult;

        var mappedModel = _mapper.Map<AppUser>(user);
        mappedModel.UserName = user.Email;
        mappedModel.PasswordHash = user.Password;

        var result = await _userManagement.CreateUser(mappedModel);
        if (!result)
            return new ServiceResponse
            {
                Message = "Email Adderss might be already in use or unknown error occurred"
            };

        var _user = await _userManagement.GetUserByEmail(user.Email);
        var users = await _userManagement.GetAllUsers();
        bool assignedResult = await _roleManagement
            .AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");
        if (!assignedResult)
        {
            //remove user
            int removeUserResult = await _userManagement
                .RemoveUserByEmail(_user!.Email!);
            if (removeUserResult <= 0)
            {
                // error occurred roling back change
                // then log th error
                _logger.LogError(new Exception(
                    $"User with Email as {_user.Email} " +
                    $"faild to remove as result of role assigning issue"),
                    "User could not be assigend Role");
                return new ServiceResponse
                {
                    Message = "Error occurred in create account"
                };
            }
        }
        return new ServiceResponse
        {
            Success = true,
            Message = "Account Created!"
        };
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="user">The user to log in.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a <see cref="LoginResponse"/> indicating the result of the login operation.</returns>
    public async Task<LoginResponse> LoginUser(LoginUserDto user)
    {
        var _validationResult = await _validationService
            .ValidateAsync(user, _loginUserValidator);
        if (!_validationResult.Success)
            return new LoginResponse(Message: _validationResult.Message);

        var mappedModel = _mapper.Map<AppUser>(user);
        mappedModel.PasswordHash = user.Password;

        bool loginResult = await _userManagement.LoginUser(mappedModel);
        if (!loginResult)
            return new LoginResponse
                (Message: "Email not found or invalid credentails");

        var _user = await _userManagement.GetUserByEmail(user.Email);
        var claims = await _userManagement.GetUserClaims(_user!.Email!);

        string jwtToken = _tokenManagement.GenerateToken(claims);
        string refreshToken = _tokenManagement.GetRefreshToken();

        bool userTokenCheck = await _tokenManagement
            .ValidateRefreshToken(refreshToken);
        int saveTokenResult = 0;

        if (userTokenCheck)
            saveTokenResult = await _tokenManagement
                .UpdateRefreshToken(_user.Id, refreshToken);
        else
            saveTokenResult = await _tokenManagement
                .AddRefreshToken(_user.Id, refreshToken);

        return saveTokenResult <= 0
            ? new LoginResponse(Message: "Internal error occurred while auth")
            : new LoginResponse(Success: true, Token: jwtToken, RefreshToken: refreshToken);
    }

    /// <summary>
    /// Revives a user's token using a refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a <see cref="LoginResponse"/> indicating the result of the token revival operation.</returns>
    public async Task<LoginResponse> ReviveTokenUser(string refreshToken)
    {
        var validationResult = await _tokenManagement
            .ValidateRefreshToken(refreshToken);
        if (!validationResult)
            return new LoginResponse(Message: "Invalid Token");

        string userId = await _tokenManagement
            .GetUserIdByRefreshToken(refreshToken);
        AppUser? user = await _userManagement.GetUserById(userId);
        var claims = await _userManagement.GetUserClaims(user!.Email!);
        string newjwtToken = _tokenManagement.GenerateToken(claims);
        string newRefreshToken = _tokenManagement.GetRefreshToken();

        await _tokenManagement
            .UpdateRefreshToken(userId, newRefreshToken);
        return new LoginResponse
            (Success: true, Token: newjwtToken, RefreshToken: newRefreshToken);
    }
}
