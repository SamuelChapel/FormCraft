using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Business.Contracts.Responses.Question;


namespace FormCraft.WebApp.ViewModels
{
    public class QuestionDetailsViewModel
    {
        public QuestionResponse Question { get; set; } = null!;
        public List<AnswerResponse> Answers { get; set; } = [];
    }
}
