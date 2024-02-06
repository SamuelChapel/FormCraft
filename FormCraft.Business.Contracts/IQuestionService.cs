using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Business.Contracts.Responses.Question;

namespace FormCraft.Business.Contracts
{
    public interface IQuestionService
    {
        /// <summary>
        /// Functon that create a question using the dto CreatequestionRequest return a dto questionResponse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<QuestionResponse> Create(CreateQuestionRequest request);
        /// <summary>
        /// Functon that Delete a question using the dto DeletequestionRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task Delete(DeleteQuestionRequest request);
        /// <summary>
        /// Functon that get All question and return a list of dto questionResponse
        /// </summary>
        /// <returns></returns>
        public Task<List<QuestionResponse>> GetAll();
        /// <summary>
        /// Function that return a dto questionResponse by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<QuestionResponse> GetById(string id);
        /// <summary>
        /// Function that update an question with a dto questionrequest and return the updated question in a dto questionResponse 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<QuestionResponse> Update(UpdateQuestionRequest request);
    }
}
