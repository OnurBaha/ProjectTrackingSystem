using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.AuthRequests;
using Business.Dtos.Responses.AuthResponses;
using Business.Dto.Request.User;
using Business.Rules.BusinessRules;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concretes;
using System;
using System.Threading.Tasks;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;
    private readonly UserBusinessRules _userBusinessRules;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper, UserBusinessRules userBusinessRules)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _mapper = mapper;
        _userBusinessRules = userBusinessRules;
    }

    public async Task<LoginResponse> Register(RegisterAuthRequest registerAuthRequest, string password)
    {
        await _userBusinessRules.IsExistsUserMail(registerAuthRequest.Email);

        User user = _mapper.Map<User>(registerAuthRequest);

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        CreateUserRequest createUserRequest = _mapper.Map<CreateUserRequest>(user);

        var addedUser = await _userService.Add(createUserRequest);
        var getUserResponse = await _userService.GetById(addedUser.Id);

        User mappedUser = _mapper.Map<User>(getUserResponse);

        var result = await CreateAccessToken(mappedUser);
        return result;
    }


    public async Task<User> Login(LoginAuthRequest loginAuthRequest)
    {
        var user = await _userService.GetByMailAsync(loginAuthRequest.Email);

        HashingHelper.VerifyPasswordHash(loginAuthRequest.Password, user.PasswordHash, user.PasswordSalt);

        var mappedUser = _mapper.Map<User>(user);
        return mappedUser;
    }

    public async Task<LoginResponse> CreateAccessToken(User user)
    {
        var accessToken = _tokenHelper.CreateToken(user);
        LoginResponse loginResponse = _mapper.Map<LoginResponse>(accessToken);

        return loginResponse;
    }

}
