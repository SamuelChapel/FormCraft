using AutoMapper;
using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;

namespace FormCraft.Business.Services
{
    public class FormBusiness : IFormBusiness
    {
        readonly IFormRepository _formRepository;
        readonly IMapper _mapper;

        public FormBusiness(IFormRepository formRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;
        }

        public async Task<FormResponse> Create(CreateFormRequest request)
        {
            var form = _mapper.Map<Form>(request);

            form.Id = Guid.NewGuid().ToString();

            form.StatusId = StatusEnum.InProgress;

            await _formRepository.Create(form);

            var formResponse = _mapper.Map<FormResponse>(form);

            return formResponse;
        }

        //Add Validate method ?
        //Add Close method ?

        public async Task Delete(DeleteFormRequest request)
        {
            var formToDelete = await GetById(request.Id);

            if (formToDelete.StatusId != StatusEnum.InProgress)
                throw new Exception("Form status not available");

            await _formRepository.Delete(_mapper.Map<Form>(formToDelete));
        }

        public async Task<List<FormResponse>> GetAll()
            => _mapper.Map<List<FormResponse>>(await _formRepository.GetAll());

        public async Task<FormWithQuestionsResponse> GetById(string id)
        {
            var form = await _formRepository.GetById(id);

            return form is null ? throw new NotFoundException("Form not found") : _mapper.Map<FormWithQuestionsResponse>(form);
        }

        public async Task<FormResponse> Update(UpdateFormRequest request)
        {
            var formToUpdate = await GetById(request.Id);

            if (formToUpdate.StatusId != StatusEnum.InProgress
                || request.StatusId != null
                && (formToUpdate.StatusId == StatusEnum.Validated && request.StatusId != StatusEnum.Closed || formToUpdate.StatusId == StatusEnum.InProgress && request.StatusId != StatusEnum.Validated))
            {
                throw new BadRequestException("Form status not available");
            }

            var form = _mapper.Map<Form>(formToUpdate);

            form.Label = request.Label ?? form.Label;
            form.StatusId = request.StatusId ?? form.StatusId;
            form.FormTypeId = request.FormTypeId ?? form.FormTypeId;

            await _formRepository.Update(form);

            return _mapper.Map<FormResponse>(form);
        }

        public async Task<List<SearchFormResponse>> Search(SearchFormRequest searchRequest)
        {
            _mapper.Map<Form>(searchRequest);

            var forms = await _formRepository.Search(searchRequest.FormTypeId, searchRequest.StatusId, searchRequest.Label, searchRequest.Order);

            return _mapper.Map<List<SearchFormResponse>>(forms);
        }
    }
}
