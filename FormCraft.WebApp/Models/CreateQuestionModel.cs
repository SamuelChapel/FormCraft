namespace FormCraft.WebApp.Models;

public class CreateQuestionModel
{
    public int Number { get; set; }
    public string Label { get; set; } = null!;
    public int QuestionTypeId { get; set; }
    public string FormId { get; set; } = null!;

    public List<CreateAnswerModel> Answers { get; set; } = [];
}
