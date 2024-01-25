using FormCraft.Business.Contracts;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Question;
namespace FormCraft.Business.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _QuestionRepository;
        public QuestionService(IQuestionRepository QuestionRepository)
        {
            _QuestionRepository = QuestionRepository;
        }
        public async Task<Question> Create(CreateQuestionRequest request)
        {
            return await _QuestionRepository.Create(new Question() { Id = Guid.NewGuid().ToString(),Label = request.Label, Number = request.Number, QuestionTypeId = request.QuestionTypeId, FormId=request.FormId });
        }
        public async Task Delete(string id)
        {
            var question = await GetById(id);

            await _QuestionRepository.Delete(question);
        }

        public async Task<List<Question>> GetAll()
        {
            return await _QuestionRepository.GetAll();
        }

        public async Task<Question> GetById(string id)
        {
            if (await _QuestionRepository.GetById(id) is Question u)
            {
                return u;
            }

            throw new NotFoundException("Question non trouvé");
        }

        public async Task<Question> Update(UpdateQuestionRequest request)
        {
            Question question = await GetById(request.Id);

            question.Label = request.Label ?? question.Label;
            question.Number = request.Number ?? question.Number;
            question.QuestionTypeId = request.QuestionTypeId ?? question.QuestionTypeId;

            return await _QuestionRepository.Update(question);
        }
    }
}
