using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Response.Answer;
using FormCraft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business.Contracts
{
    public interface IAnswerBusiness
    {
        public Task<AnswerResponse> Create(CreateAnswerRequest request);
        public Task Delete(DeleteAnswerRequest request);
        public Task<List<AnswerResponse>> GetAll();
        public Task<AnswerResponse> GetById(string id);
        public Task<AnswerResponse> Update(UpdateAnswerRequest request);
    }
}
