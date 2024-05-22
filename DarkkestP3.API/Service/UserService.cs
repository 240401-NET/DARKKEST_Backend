using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using DarkkestP3.API.Repository;
using Microsoft.AspNetCore.Identity;

namespace DarkkestP3.API.Service;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
    }

    public async Task<IdentityResult> RegisterUser(RegisterUser registration)
    {
        ApplicationUser user = new()
        {
            UserName = registration.Username,
            Email = registration.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, registration.Password);
        return result;
    }

    public async Task<SignInResult> LoginUser(LoginUser login)
    {
        // var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
        // var isPersistent = (useCookies == true) && (useSessionCookies != true);
        _signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;

        var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, isPersistent: true, lockoutOnFailure: true);

        return result;
    }

    public async void Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public string GetUserIdByName(string username)
    {
        return _userRepository.GetUserIdByName(username);
    }
}