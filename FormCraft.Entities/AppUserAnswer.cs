using System.ComponentModel.DataAnnotations.Schema;
using FormCraft.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.Entities;

[PrimaryKey(nameof(AppUserId), nameof(AnswerId))]
public class AppUserAnswer : IDated
{
    [ForeignKey(nameof(AppUser))]
    public string AppUserId { get; set; } = null!;
    public AppUser? AppUser { get; set; } = null!;

    [ForeignKey(nameof(Answer))]
    public string AnswerId { get; set; } = null!;
    public Answer? Answer { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}