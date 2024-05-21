

using Darkkest.API.DB;
using Darkkest.API.DTO;
using Darkkest.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Darkkest.API.Repository;

public class ProfileRepository : IProfileRepository
{
    private readonly CommunityDBContext _comContext;
    private readonly UserDBContext _userContext;

    public ProfileRepository(CommunityDBContext comContext, UserDBContext userContext)
    {
        _comContext = comContext;
        _userContext = userContext;
    }

    public Profile AddUserProfile(Profile newProfile)
    {
        _comContext.Profiles.Add(newProfile);
        _comContext.SaveChanges();
        return newProfile;
    }

    public async Task<Profile> GetUserProfileByUserId(int userId)
    {
        return await _comContext.Profiles.Where(profile => profile.UserId == userId).SingleAsync();
    }

    public async Task<Profile> UpdateUserProfile(UpdateProfile updateProfile)
    {
        Profile oldProfile = await _comContext.Profiles.Where(profile => profile.UserId == updateProfile.userId).SingleAsync();
        oldProfile.Interersts = updateProfile.updatedInterests;
        oldProfile.Interersts = updateProfile.updatedSkills;
        oldProfile.MissionStatement = updateProfile.updatedMissionStatement;
        _comContext.SaveChanges();
        return oldProfile;
    }

    public async Task<Profile> UpdateUserProfileInterests(PatchProfileInterests patchProfile)
    {
        Profile oldProfile = await _comContext.Profiles.Where(profile => profile.UserId == patchProfile.userId).SingleAsync();
        oldProfile.Interersts = patchProfile.updatedInterests;
        _comContext.SaveChanges();
        return oldProfile;
    }

        public async Task<Profile> UpdateUserProfileSkills(PatchProfileSkills patchProfile)
    {
        Profile oldProfile = await _comContext.Profiles.Where(profile => profile.UserId == patchProfile.userId).SingleAsync();
        oldProfile.Skills = patchProfile.updatedSkills;
        _comContext.SaveChanges();
        return oldProfile;
    }

        public async Task<Profile> UpdateUserProfileMissionStatement(PatchProfileMissionStatement patchProfile)
    {
        Profile oldProfile = await _comContext.Profiles.Where(profile => profile.UserId == patchProfile.userId).SingleAsync();
        oldProfile.MissionStatement = patchProfile.updatedMissionStatement;
        _comContext.SaveChanges();
        return oldProfile;
    }

}