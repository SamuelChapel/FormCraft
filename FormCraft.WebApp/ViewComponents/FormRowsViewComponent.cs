using FormCraft.WebApp.ViewModels.FormViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents
{
    public class FormRowsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<FormResponseViewModel> forms)
        {
            return View(forms); //default
        }
    }
}
