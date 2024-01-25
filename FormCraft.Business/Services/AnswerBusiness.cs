using AutoMapper;
using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;

namespace FormCraft.Business
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
            var answerToDelete = await GetById(request.Id);
            var answer = _mapper.Map<Answer>(answerToDelete);

            if (answerToDelete is not null)
            {
                await _answerRepository.Delete(answer);
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
            var answerToUpdate = await GetById(request.Id);
            var answer = _mapper.Map<Answer>(answerToUpdate);

            answer.Label = request.Label;

            await _answerRepository.Update(answer);

            return _mapper.Map<AnswerResponse>(answerToUpdate);
        }
    }
}
