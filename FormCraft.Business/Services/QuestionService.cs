using AutoMapper;
using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Business.Contracts.Responses.Question;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;

namespace FormCraft.Business.Services;

public class QuestionService : IQuestionService
{
    private readonly IFormBusiness _formBusiness;
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IFormBusiness formBusiness)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
        _formBusiness = formBusiness;
    }

    public async Task<QuestionResponse> Create(CreateQuestionRequest request)
    {
        var form = await _formBusiness.GetById(request.FormId);
        if (form.StatusId != StatusEnum.InProgress)
            throw new BadRequestException("From status not available");

        var question = _mapper.Map<Question>(request);
        question.Id = Guid.NewGuid().ToString();
        question = await _questionRepository.Create(question);

        return _mapper.Map<QuestionResponse>(question);
    }

    public async Task Delete(DeleteQuestionRequest request)
    {
        var questionToDelete = await GetById(request.Id);

        var form = await _formBusiness.GetById(questionToDelete.FormId);

        if (form.StatusId != StatusEnum.InProgress)
            throw new BadRequestException("From status not available");

        if (questionToDelete is not null)
        {
            var question = _mapper.Map<Question>(questionToDelete);
            await _questionRepository.Delete(question);
        }
        throw new NotFoundException("Question not found");
    }

    public async Task<List<QuestionResponse>> GetAll()
    => _mapper.Map<List<QuestionResponse>>(await _questionRepository.GetAll());

    public async Task<QuestionResponse> GetById(string id)
    {
        if (await _questionRepository.GetById(id) is Question q)
        {
            return _mapper.Map<QuestionResponse>(q);
        }
        throw new NotFoundException("Question not found");
    }

    public async Task<QuestionResponse> Update(UpdateQuestionRequest request)
    {
        var questionToUpdate = await GetById(request.Id);

        var form = await _formBusiness.GetById(questionToUpdate.FormId);

        if (form.StatusId != StatusEnum.InProgress)
            throw new BadRequestException("From status not available");

        var question = _mapper.Map<Question>(questionToUpdate);
        question.Label = request.Label ?? question.Label;
        await _questionRepository.Update(question);

        return _mapper.Map<QuestionResponse>(question);
    }
}
