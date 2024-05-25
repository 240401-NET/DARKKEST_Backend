using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;

namespace DarkkestP3.API.Repository;

public interface IUserRepository
{
    string GetUserIdByName(string username);
    ApplicationUser GetUser(string username);
}

public interface IOpportunityRepository
{
    IEnumerable<Opportunity> GetAllOpps();
    IEnumerable<Opportunity> GetUserOpps(string userId);
    Opportunity GetOppById(int id);
    Opportunity CreateOpp(Opportunity newOpp);
    Opportunity UpdateOpp(Opportunity updateOpp);
    Opportunity DeleteOpp(Opportunity deleteOpp);
}

public interface IProfileRepository
{
    Profile AddUserProfile(Profile newProfile);
    Task<Profile> DeleteUserProfile(string userId);
    Task<Profile> GetUserProfileByUserId(string userId);
    Task<Profile> UpdateUserProfile(UpdateProfile updateProfile);
    Task<Profile> UpdateUserProfileInterests(PatchProfileInterests patchProfile);
    Task<Profile> UpdateUserProfileMissionStatement(PatchProfileMissionStatement patchProfile);
    Task<Profile> UpdateUserProfileSkills(PatchProfileSkills patchProfile);
}