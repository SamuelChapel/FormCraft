using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Business.Contracts.Responses.Question;

namespace FormCraft.Business.Contracts
{
    public interface IQuestionService
    {
        public Task<QuestionResponse> Create(CreateQuestionRequest request);
        public Task Delete(DeleteQuestionRequest request);
        public Task<List<QuestionResponse>> GetAll();
        public Task<QuestionResponse> GetById(string id);
        public Task<QuestionResponse> Update(UpdateQuestionRequest request);
    }
}
