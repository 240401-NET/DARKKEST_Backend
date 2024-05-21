using Darkkest.API.Model;

namespace Darkkest.API.Repository;

public interface IUserRepository
{
    
}

public interface IProfileRepository
{
    Profile AddUserProfile(Profile newProfile);
    Task<Profile> GetUserProfileByUserId(int userId);
}