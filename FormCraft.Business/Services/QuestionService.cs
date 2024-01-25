using FormCraft.Business.Contracts;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Question;
using AutoMapper;
using FormCraft.Business.Contracts.Response.Question;
namespace FormCraft.Business.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public QuestionService(IQuestionRepository questionRepository,IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<QuestionResponse> Create(CreateQuestionRequest request)
        {
            var question = _mapper.Map<Question>(request);
            question.Id = Guid.NewGuid().ToString();
            await _questionRepository.Create(question);

            return _mapper.Map<QuestionResponse>(question);
        }
        public async Task Delete(DeleteQuestionRequest request)
        {
            var questionToDelete = await GetById(request.Id);
            var question = _mapper.Map<Question>(questionToDelete);

            if (questionToDelete is not null)
            {
                await _questionRepository.Delete(question);
            }
        }

        public async Task<List<QuestionResponse>> GetAll()
        => _mapper.Map<List<QuestionResponse>>(await _questionRepository.GetAll());

        public async Task<QuestionResponse> GetById(string id)
        {
            if (await _questionRepository.GetById(id) is Question u)
            {
                return _mapper.Map<QuestionResponse>(u); 
            }
            throw new NotFoundException("Question non trouvé");
        }

        public async Task<QuestionResponse> Update(UpdateQuestionRequest request)
        {
            var questionToUpdate = await GetById(request.Id);
            var question = _mapper.Map<Question>(questionToUpdate);
            question.Label = request?.Label?? question.Label;
            await _questionRepository.Update(question);

            return _mapper.Map<QuestionResponse>(question);
        }
    }
}
