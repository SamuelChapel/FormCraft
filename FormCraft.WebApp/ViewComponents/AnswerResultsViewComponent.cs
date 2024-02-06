using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.WebApp.ViewModels.FormViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents
{
    public class AnswerResultsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<AnswerResultResponse> result)
        {
            return View(result);
        }
    }
}
