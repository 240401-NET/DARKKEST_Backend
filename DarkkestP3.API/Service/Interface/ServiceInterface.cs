using Darkkest.API.DTO;
using Microsoft.AspNetCore.Identity;

namespace Darkkest.API.Service;

public interface IUserService
{
    Task<IdentityResult> RegisterUser(RegisterUser registration);
    Task<SignInResult> LoginUser(LoginUser login);
    void Logout();
}

public interface IProfileService
{
    
}