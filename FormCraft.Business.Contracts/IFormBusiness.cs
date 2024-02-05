using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;

namespace FormCraft.Business.Contracts
{
    public interface IFormBusiness
    {
        public Task<FormResponse> Create(CreateFormRequest request);
        public Task Delete(DeleteFormRequest request, string creatorId, bool isAdmin);
        public Task<List<FormResponse>> GetAll();
        public Task<FormWithQuestionsResponse> GetById(string id);
        public Task<FormResponse> Update(UpdateFormRequest request);
        public Task<List<FormResponse>> Search(SearchFormRequest searchRequest);
        public Task<FormWithQuestionsResponse> Duplicate(string id, string creatorId);

    }
}
