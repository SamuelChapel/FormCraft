using FormCraft.WebApp.Models;

namespace FormCraft.WebApp.ViewModels;

public class FormViewModel
{
    public CreateFormModel CreateFormModel { get; set; } = new();
    public List<QuestionDetailsViewModel> Questions { get; set; } = [];
}
