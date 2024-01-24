using FormCraft.Entities.Common;

namespace FormCraft.Entities;

public class Form : Entity, IDated
{
    public string Label { get; set; } = null!;
    public AppUser Creator { get; set; } = null!;

    public int FormTypeId { get; set; }
    public FormType FormType { get; set; } = null!;

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public List<Question> Questions { get; set; } = [];

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
