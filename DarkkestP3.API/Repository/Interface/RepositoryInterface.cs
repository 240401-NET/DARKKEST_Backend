using Darkkest.API.DTO;
using Darkkest.API.Model;

namespace Darkkest.API.Repository;

public interface IUserRepository
{
    
}

public interface IProfileRepository
{
    Profile AddUserProfile(Profile newProfile);
    Task<Profile> GetUserProfileByUserId(int userId);
    Task<Profile> UpdateUserProfileInterests(PatchProfileInterests patchProfile);
    Task<Profile> UpdateUserProfileMissionStatement(PatchProfileMissionStatement patchProfile);
    Task<Profile> UpdateUserProfileSkills(PatchProfileSkills patchProfile);
}