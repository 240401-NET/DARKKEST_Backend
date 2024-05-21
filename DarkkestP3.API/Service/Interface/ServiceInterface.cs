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

public interface IOpportunityService
{
    IEnumerable<Opportunity> GetAllOpps();
    Opportunity GetOppById(int id);
    Opportunity CreateOpp(NewOpp newOpp, string userId);
    Opportunity UpdateOpp(Opportunity updateOpp);
}