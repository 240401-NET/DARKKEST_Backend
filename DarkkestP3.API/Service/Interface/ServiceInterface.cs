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
    Task<Profile> GetUserProfileByUserId(int userId);
    Task<Profile> UpdateUserProfile(int userId);
    Task<Profile> UpdateUserProfileInterests(PatchProfileInterests patchProfile);
    Task<Profile> UpdateUserProfileMissionStatement(PatchProfileMissionStatement patchProfile);
    Task<Profile> UpdateUserProfileSkills(PatchProfileSkills patchProfile);
}