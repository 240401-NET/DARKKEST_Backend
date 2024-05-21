

using Darkkest.API.DB;
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
}