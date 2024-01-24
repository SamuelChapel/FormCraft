using System.ComponentModel.DataAnnotations.Schema;
using FormCraft.Entities.Common;

namespace FormCraft.Entities;

public class Answer : Entity, IDated
{
    public string Label { get; set; } = null!;
    public int Total { get; set; }

    [ForeignKey(nameof(Question))]
    public string QuestionId { get; set; } = null!;
    public Question Question { get; set; } = null!;

    public List<AppUserAnswer> AppUserAnswers { get; set; } = [];

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Answer()
    {
    }
}
