using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts
{
    public interface IQuestionService
    {
        public Task<Question> GetById(string id);
        public Task<List<Question>> GetAll();
        public Task<Question> Create(CreateQuestionRequest request);
        public Task<Question> Update(UpdateQuestionRequest request);
        public Task Delete(string id);
    }
}
