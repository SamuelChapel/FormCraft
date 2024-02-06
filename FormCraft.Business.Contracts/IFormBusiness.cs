using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;

namespace FormCraft.Business.Contracts
{
    public interface IFormBusiness
    {
        /// <summary>
        /// Functon that create a Question using the dto CreateQuestionRequest return a dto QuestionResponse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<FormResponse> Create(CreateFormRequest request);
        /// <summary>
        /// Functon that Delete a Question using the dto DeleteQuestionRequest
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creatorId"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        public Task Delete(DeleteFormRequest request, string creatorId, bool isAdmin);
        /// <summary>
        /// Functon that get All form and return a list of dto formResponse
        /// </summary>
        /// <returns></returns>
        public Task<List<FormResponse>> GetAll();
        /// <summary>
        /// Function that return a dto formResponse by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<FormWithQuestionsResponse> GetById(string id);
        /// <summary>
        /// Function that update an form with a dto updateformrequest and return the updated form in a dto formResponse 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<FormResponse> Update(UpdateFormRequest request);
        /// <summary>
        /// Function that research an appuserform with a dto SearchformRequest and return a list of dto
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        public Task<List<FormResponse>> Search(SearchFormRequest searchRequest);
        /// <summary>
        /// Function that duplicate a form
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public Task<FormWithQuestionsResponse> Duplicate(string id, string creatorId);
        /// <summary>
        /// Function that count the number of the same response to the same question and return the count
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> SounderCount(string id);

    }
}
