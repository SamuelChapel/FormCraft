using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;

namespace FormCraft.Business.Contracts
{
    public interface IFormBusiness
    {
        public Task<FormResponse> Create(CreateFormRequest request);
        public Task Delete(DeleteFormRequest request);
        public Task<List<FormResponse>> GetAll();
        public Task<FormWithQuestionsResponse> GetById(string id);
        public Task<FormResponse> Update(UpdateFormRequest request);
    }
}
