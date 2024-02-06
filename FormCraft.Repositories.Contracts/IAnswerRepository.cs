using FormCraft.Entities;
using FormCraft.Repositories.Contracts.Common;
using FormCraft.Repositories.Contracts.Response;

namespace FormCraft.Repositories.Contracts
{
    public interface IAnswerRepository : IReadRepository<Answer> ,IWriteRepository<Answer>
    {
        /// <summary>
        /// Function that create an appuseranswer in the database with entity framework
        /// </summary>
        /// <param name="userAnswer"></param>
        /// <returns></returns>
        public Task AddUserAnswer(AppUserAnswer userAnswer);
        /// <summary>
        /// Function that return a list of answerResultResponseRepository by their question and form
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public Task<List<AnswerResultResponseRepository>> ChoiceByQuestion(string formId, string questionId);
    }
}
