using FormCraft.Business.Contracts.Responses.Form;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents;

public class FormViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FormWithQuestionsResponse form)
    {
        return View(form);
    }
}