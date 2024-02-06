using AutoMapper;
using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;

namespace FormCraft.Business.Services
{
    public class FormBusiness(IFormRepository formRepository, IMapper mapper) : IFormBusiness
    {
        readonly IFormRepository _formRepository = formRepository;
        readonly IMapper _mapper = mapper;

        public async Task<FormResponse> Create(CreateFormRequest request)
        {
            var form = _mapper.Map<Form>(request);

            form.Id = Guid.NewGuid().ToString();

            form.StatusId = StatusEnum.InProgress;

            form = await _formRepository.Create(form);

            var formResponse = _mapper.Map<FormResponse>(form);

            return formResponse;
        }

        public async Task Delete(DeleteFormRequest request, string creatorId, bool isAdmin)
        {
            var formToDelete = await _formRepository.GetById(request.Id) ?? throw new NotFoundException("Form not found");

            if (!isAdmin && formToDelete.CreatorId != creatorId)
                throw new BadRequestException("Delete form not authorize");

            if (formToDelete.StatusId != StatusEnum.InProgress)
                throw new Exception("Form status not available");

            await _formRepository.Delete(formToDelete);
        }

        public async Task<List<FormResponse>> GetAll()
        {
            return _mapper.Map<List<FormResponse>>(await _formRepository.GetAll());
        }

        public async Task<FormWithQuestionsResponse> GetById(string id)
        {
            var form = await _formRepository.GetById(id);

            return form is null ? throw new NotFoundException("Form not found") : _mapper.Map<FormWithQuestionsResponse>(form);
        }

        public async Task<FormResponse> Update(UpdateFormRequest request)
        {
            var form = await _formRepository.GetById(request.Id) ?? throw new NotFoundException("Form not found");

            if (form.StatusId != StatusEnum.InProgress
                || request.StatusId != null
                && (form.StatusId == StatusEnum.Validated && request.StatusId != StatusEnum.Closed || form.StatusId == StatusEnum.InProgress && request.StatusId != StatusEnum.Validated))
            {
                throw new BadRequestException("Form status not available");
            }

            form.Label = request.Label ?? form.Label;
            form.StatusId = request.StatusId ?? form.StatusId;
            form.FormTypeId = request.FormTypeId ?? form.FormTypeId;

            await _formRepository.Update(form);

            return _mapper.Map<FormResponse>(form);
        }

        public async Task<List<FormResponse>> Search(SearchFormRequest searchRequest)
        {
            var forms = await _formRepository.Search(searchRequest.IsStatusEnumPicked, searchRequest.IsFormTypePicked, searchRequest.Label, searchRequest.Order, searchRequest.CurrentUserId);

            return _mapper.Map<List<FormResponse>>(forms);
        }

        public async Task<FormWithQuestionsResponse> Duplicate(string id, string creatorId)
        {
            var formToDuplicate = await _formRepository.GetById(id) ?? throw new NotFoundException("Form not found");

            formToDuplicate.Id = Guid.NewGuid().ToString();
            formToDuplicate.CreatorId = creatorId;
            formToDuplicate.StatusId = StatusEnum.InProgress;

            var formDuplicated = await _formRepository.Create(formToDuplicate);

            return _mapper.Map<FormWithQuestionsResponse>(formDuplicated);
        }

        public async Task<int> SounderCount(string id)
        {
            return await _formRepository.SounderCount(id);
        }
    }
}
