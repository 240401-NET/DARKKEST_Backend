using Darkkest.API.DTO;
using Darkkest.API.Model;
using Darkkest.API.Repository;

namespace Darkkest.API.Service;

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

    public object DeleteUserProfile(int userId)
    {
        throw new NotImplementedException();
    }

    public Task GetUserProfileByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public object UpdateUserProfile(int userId)
    {
        throw new NotImplementedException();
    }
}