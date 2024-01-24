using FormCraft.Entities;
using FormCraft.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business.Contracts.Response.Answer
{
    public class AnswerResponse : IDated
    {
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
