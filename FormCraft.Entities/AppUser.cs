using Microsoft.AspNetCore.Identity;

namespace FormCraft.Entities;

public class AppUser : IdentityUser
{
    public string Address { get; set; } = null!;
}
