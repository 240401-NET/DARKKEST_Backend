using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using DarkkestP3.API.Repository;

namespace DarkkestP3.API.Service;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepository;

    public ProfileService(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }
    public async Task<Profile> CreateUserProfile(NewProfile newProfile)
    {
        Profile createdProfile = new Profile 
        {
            UserId = newProfile.userId,
            Interersts = newProfile.interests,
            Skills = newProfile.skills,
            MissionStatement = newProfile.missionStatement
        };

        var result = _profileRepository.AddUserProfile(createdProfile);
        return result;
        
    }


    public async Task<Profile> GetUserProfileByUserId(string userId)
    {
        return await _profileRepository.GetUserProfileByUserId(userId);
    }

    public Task<Profile> UpdateUserProfile(UpdateProfile updateProfile)
    {
        var result = _profileRepository.UpdateUserProfile(updateProfile);
        return result;
    }

    public Task<Profile> UpdateUserProfileInterests(PatchProfileInterests patchProfile)
    {
        var result = _profileRepository.UpdateUserProfileInterests(patchProfile);
        return result;
    }

    public Task<Profile> UpdateUserProfileSkills(PatchProfileSkills patchProfile)
    {
        var result = _profileRepository.UpdateUserProfileSkills(patchProfile);
        return result;
    }

    public Task<Profile> UpdateUserProfileMissionStatement(PatchProfileMissionStatement patchProfile)
    {
        var result = _profileRepository.UpdateUserProfileMissionStatement(patchProfile);
        return result;
    }
    public Task<Profile> DeleteUserProfile(string userId)
    {
        var result = _profileRepository.DeleteUserProfile(userId);
        return result;
    }
}