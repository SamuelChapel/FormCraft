using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Response.Answer;
using FormCraft.Business.Contracts.Response.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business.Contracts
{
    public interface IFormBusiness
    {
        public Task<FormResponse> Create(CreateFormRequest request);
        public Task Delete(DeleteFormRequest request);
        public Task<List<FormResponse>> GetAll();
        public Task<FormResponse> GetById(string id);
        public Task<FormResponse> Update(UpdateFormRequest request);
    }
}
