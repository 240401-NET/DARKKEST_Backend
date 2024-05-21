using Darkkest.API.DTO;
using Microsoft.AspNetCore.Identity;

namespace Darkkest.API.Service;

public interface IUserService
{
    Task<IdentityResult> RegisterUser(RegisterUser registration);
    Task<SignInResult> LoginUser(LoginUser login);
    void Logout();
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