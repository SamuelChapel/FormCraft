using FormCraft.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormCraft.Business.Contracts.Requests.Answer
{
    public record DeleteAnswerRequest
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = null!;
        public int Total { get; set; }

        [ForeignKey(nameof(Question))]
        public string QuestionId { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public List<AppUserAnswer> AppUserAnswers { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}