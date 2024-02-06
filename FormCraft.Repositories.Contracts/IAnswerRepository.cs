using FormCraft.Entities;
using FormCraft.Repositories.Contracts.Common;
using FormCraft.Repositories.Contracts.Response;

namespace FormCraft.Repositories.Contracts
{
    public interface IAnswerRepository : IReadRepository<Answer> ,IWriteRepository<Answer>
    {
        public Task AddUserAnswer(AppUserAnswer userAnswer);
        public Task<AnswerResultResponseRepository> ChoiceByQuestion(string formId, int questionId);
    }
}
