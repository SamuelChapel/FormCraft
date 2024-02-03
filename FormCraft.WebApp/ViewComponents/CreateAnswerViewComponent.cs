using FormCraft.Business.Contracts.Responses.Answer;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents;

public class CreateAnswerViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(AnswerResponse answer)
    {
        var view = View("CreateAnswer", answer);
        return view;
    }
}
