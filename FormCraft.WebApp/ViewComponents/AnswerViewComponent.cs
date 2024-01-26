using FormCraft.Business.Contracts.Responses.Answer;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents
{
    public class AnswerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(AnswerResponse response) 
        {
            return View(response);
        
        }
    }
}
