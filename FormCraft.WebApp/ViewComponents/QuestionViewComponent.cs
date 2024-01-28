using FormCraft.Business.Contracts.Responses.Question;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents;

public class QuestionViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(QuestionResponse question)
    {
        return View(question);
    }
}
