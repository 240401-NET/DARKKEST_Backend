using Darkkest.API.DTO;
using Darkkest.API.Model;
using Microsoft.AspNetCore.Identity;

namespace Darkkest.API.Service;

public interface IUserService
{
    Task<IdentityResult> RegisterUser(RegisterUser registration);
    Task<SignInResult> LoginUser(LoginUser login);
    void Logout();
}

public interface IProfileService
{
    Task<Profile> CreateUserProfile(NewProfile newProfileDTO);
    object DeleteUserProfile(int userId);
    Task GetUserProfileByUserId(int userId);
    object UpdateUserProfile(int userId);
}