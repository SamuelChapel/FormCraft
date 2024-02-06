using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts
{
    public interface IAnswerBusiness
    {
        public Task<AnswerResponse> Create(CreateAnswerRequest request);
        public Task Delete(DeleteAnswerRequest request);
        public Task<List<AnswerResponse>> GetAll();
        public Task<AnswerResponse> GetById(string id);
        public Task<AnswerResponse> Update(UpdateAnswerRequest request);
        public Task AddUserAnswer(CreateUserAnswerRequest request);

        public Task<AnswerResultResponse> ChoiceByQuestion(string formId, int questionId);
    }
}
