using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using Microsoft.AspNetCore.Identity;

namespace DarkkestP3.API.Service;

public interface IUserService
{
    Task<IdentityResult> RegisterUser(RegisterUser registration);
    Task<SignInResult> LoginUser(LoginUser login);
    void Logout();
    string GetUserIdByName(string username);
}

public interface IApplicationService
{
    Task<IEnumerable<ApplicationDTO>> GetApplications();
    Task<ApplicationDTO> GetApplication(int appId);
    Task<IdentityResult> CreateApplication(CreateApplication createApplication);
    Task<IdentityResult> UpdateApplication(int appId, UpdateApplication updateApplication);
    Task<IdentityResult> DeleteApplication(int appId);
    Task<IEnumerable<ApplicationDTO>> GetApplicationsForUser(int userId);
    Task<IEnumerable<ApplicationDTO>> GetApplicationsForOpportunity(int opportunityId);
    Task<IdentityResult> SubmitApplication(int appId);
    Task<IdentityResult> ApproveApplication(int appId);
}

public interface IOpportunityService
{
    IEnumerable<Opportunity> GetAllOpps();
    Opportunity GetOppById(int id);
    Opportunity CreateOpp(NewOpp newOpp, string userId);
    Opportunity UpdateOpp(UpdateOpp updateOpp, string userId);
    Opportunity DeleteOpp(int id);
}

public interface IProfileService
{
    Task<Profile> CreateUserProfile(NewProfile newProfileDTO);
    Task<Profile> DeleteUserProfile(int userId);
    Task<Profile> GetUserProfileByUserId(int userId);
    Task<Profile> UpdateUserProfile(UpdateProfile updateProfile);
    Task<Profile> UpdateUserProfileInterests(PatchProfileInterests patchProfile);
    Task<Profile> UpdateUserProfileMissionStatement(PatchProfileMissionStatement patchProfile);
    Task<Profile> UpdateUserProfileSkills(PatchProfileSkills patchProfile);
}

public interface IOrganizationService
{
    Task<IdentityResult> RegisterOrganization(RegisterOrganization createOrganization);
    Task<IdentityResult> UpdateOrganization(UpdateOrganization updateOrganization);
    Task<IdentityResult> DeleteOrganization(DeleteOrganization deleteOrganization);
    Task<OrganizationDTO> GetOrganization(int orgId);
    Task<IEnumerable<OrganizationDTO>> GetOrganizations();
    Task<OrganizationDTO> GetOrganizationByName(string orgName);
}