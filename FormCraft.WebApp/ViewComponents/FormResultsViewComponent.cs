using FormCraft.WebApp.ViewModels.FormViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents
{
    public class FormResultsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(FormDetailsViewModel form)
        {
            return View(form);
        }
    }
}
