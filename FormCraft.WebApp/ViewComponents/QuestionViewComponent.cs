using FormCraft.Business.Contracts.Responses.Question;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents;

public enum QuestionViewEnum { Display, Create }

public class QuestionViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(QuestionResponse question, QuestionViewEnum view)
    {
        return view switch
        {
            QuestionViewEnum.Display => View(question),
            QuestionViewEnum.Create => View("Create", question),
            _ => View(question),
        };
    }
}
