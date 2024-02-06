using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts
{
    public interface IAnswerBusiness
    {
        /// <summary>
        /// Functon that create a Answer using the dto CreateAnswerRequest return a dto AnswerResponse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<AnswerResponse> Create(CreateAnswerRequest request);
        /// <summary>
        /// Functon that Delete a Answer using the dto DeleteAnswerRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task Delete(DeleteAnswerRequest request);
        /// <summary>
        /// Functon that get All Answer and return a list of dto AnswerResponse
        /// </summary>
        /// <returns></returns>
        public Task<List<AnswerResponse>> GetAll();
        /// <summary>
        /// Function that return a dto answerResponse by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AnswerResponse> GetById(string id);
        /// <summary>
        /// Function that update an answer with a dto answerrequest and return the updated answer in a dto answerResponse 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<AnswerResponse> Update(UpdateAnswerRequest request);
        /// <summary>
        /// Function that create an appuseranswer with a dto CreateUserAnswerRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task AddUserAnswer(CreateUserAnswerRequest request);
        /// <summary>
        /// Function that return a list of answerResultResponse
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public Task<List<AnswerResultResponse>> ChoiceByQuestion(string formId, string questionId);
    }
}
