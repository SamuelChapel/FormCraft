namespace FormCraft.WebApp.Models;

public class CreateAnswerModel
{
    public string? Id { get; set; }
    public string? Label { get; set; }
    public string QuestionId { get; set; } = null!;
}
