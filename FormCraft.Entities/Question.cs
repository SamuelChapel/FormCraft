using System.ComponentModel.DataAnnotations.Schema;
using FormCraft.Entities.Common;

namespace FormCraft.Entities;

public class Question : Entity, IDated
{
    public int Number { get; set; }
    public string Label { get; set; } = null!;

    [ForeignKey(nameof(QuestionType))]
    public int QuestionTypeId { get; set; }
    public QuestionType? QuestionType { get; set; } = null!;

    public string FormId { get; set; } = null!;

    public List<Answer> Answers { get; set; } = [];

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
