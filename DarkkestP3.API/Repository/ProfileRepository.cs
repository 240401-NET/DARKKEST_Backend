

using Darkkest.API.DB;
using Darkkest.API.Model;

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
        _comContext.Add(newProfile);
        _comContext.SaveChanges();
        return newProfile;
    }
}