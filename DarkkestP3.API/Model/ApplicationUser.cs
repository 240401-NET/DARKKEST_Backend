using Microsoft.AspNetCore.Identity;

namespace DarkkestP3.API.Model;

public class ApplicationUser : IdentityUser
{
    public bool IsOrganization { get; set; } = false;    
}