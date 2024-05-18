using Microsoft.AspNetCore.Identity;

namespace Darkkest.API.Model;

public class ApplicationUser : IdentityUser
{
    public bool IsOrganization { get; set; } = false;
    
}