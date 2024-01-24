using FormCraft.Entities.Common;

namespace FormCraft.Entities;

public class Answer : Entity, IDated
{
    public string Label { get; set; } = null!;
    public int Total { get; set; }

    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
