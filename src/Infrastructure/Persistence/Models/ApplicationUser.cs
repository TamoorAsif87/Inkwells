using Microsoft.AspNetCore.Identity;

namespace Persistence.Models;

public class ApplicationUser:IdentityUser
{
    public string Name { get; set; }
    public string? ProfileImage { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}
