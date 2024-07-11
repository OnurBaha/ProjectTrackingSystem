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
using Core.CrossCuttingConcerns.Exceptions.Types;

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

    //public async Task<LoginResponse> Register(RegisterAuthRequest registerAuthRequest, string password)
    //{
    //    try
    //    {
    //        // Check if user already exists with the same email
    //        await _userBusinessRules.IsExistsUserMail(registerAuthRequest.Email);

    //        // Map DTO to entity and hash the password
    //        User user = _mapper.Map<User>(registerAuthRequest);
    //        byte[] passwordHash, passwordSalt;
    //        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
    //        user.PasswordHash = passwordHash;
    //        user.PasswordSalt = passwordSalt;

    //        // Create CreateUserRequest
    //        CreateUserRequest createUserRequest = _mapper.Map<CreateUserRequest>(user);

    //        // Call user service to add the user
    //        var addedUser = await _userService.Add(createUserRequest);

    //        // Create access token for the registered user
    //        var result = await CreateAccessToken(user);
    //        return result;
    //    }
    //    catch (BusinessException ex)
    //    {
    //        throw new Exception($"Registration failed: {ex.Message}", ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("Registration failed.", ex);
    //    }
    //}
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

    //public async Task<User> Login(LoginAuthRequest loginAuthRequest)
    //{
    //    try
    //    {
    //        // Retrieve user from database by email
    //        var user = await _userService.GetByMailAsync(loginAuthRequest.Email);

    //        if (user == null)
    //        {
    //            throw new Exception("User not found.");
    //        }

    //        // Verify password hash
    //        if (!HashingHelper.VerifyPasswordHash(loginAuthRequest.Password, user.PasswordHash, user.PasswordSalt))
    //        {
    //            throw new Exception("Incorrect password.");
    //        }

    //        // Map to DTO and return
    //        var mappedUser = _mapper.Map<User>(user);
    //        return mappedUser;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("Login failed.", ex);
    //    }
    //}


    //public async Task<LoginResponse> CreateAccessToken(User user)
    //{
    //    // Create access token
    //    var accessToken = _tokenHelper.CreateToken(user);

    //    // Map access token to response DTO and return
    //    var loginResponse = _mapper.Map<LoginResponse>(accessToken);
    //    return loginResponse;
    //}

}
