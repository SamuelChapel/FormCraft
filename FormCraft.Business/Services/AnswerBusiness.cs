using AutoMapper;
using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;

namespace FormCraft.Business.Services
{
    public class AnswerBusiness : IAnswerBusiness
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerBusiness(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<AnswerResponse> Create(CreateAnswerRequest request)
        {
            var answer = _mapper.Map<Answer>(request);
            answer.Id = Guid.NewGuid().ToString();

            await _answerRepository.Create(answer);

            return _mapper.Map<AnswerResponse>(answer);
        }

        public async Task Delete(DeleteAnswerRequest request)
        {
            var answerToDelete = await _answerRepository.GetById(request.Id);

            if (answerToDelete is not null)
            {
                await _answerRepository.Delete(answerToDelete);
            }
        }

        public async Task<List<AnswerResponse>> GetAll()
            => _mapper.Map<List<AnswerResponse>>(await _answerRepository.GetAll());

        public async Task<AnswerResponse> GetById(string id)
        {
            try
            {
                return _mapper.Map<AnswerResponse>(await _answerRepository.GetById(id));
            }
            catch (Exception)
            {
                throw new Exception("Answer not found");
            }
        }

        public async Task<AnswerResponse> Update(UpdateAnswerRequest request)
        {
            var answerToUpdate = await _answerRepository.GetById(request.Id) ?? throw new Exception("Answer not found");

            answerToUpdate.Label = request.Label;

            answerToUpdate = await _answerRepository.Update(answerToUpdate);

            return _mapper.Map<AnswerResponse>(answerToUpdate);
        }
        public async Task AddUserAnswer(CreateUserAnswerRequest request)
        {
            if (await _answerRepository.GetById(request.AnswerId) is not null)
            {
                var userAnswer = _mapper.Map<AppUserAnswer>(request);
                await _answerRepository.AddUserAnswer(userAnswer);
            }
            else
            {
                throw new BadRequestException("The answer don't exist");
            }
            
        }
    }
}
