using FormCraft.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace FormCraft.Entities;

public class AppUser : IdentityUser, IDated
{
    public string? Address { get; set; } = null!;

    public List<AppUserAnswer> AppUserAnswers { get; set; } = [];
    public List<Form> Forms { get; set; } = [];

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
