using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Entities;
using FormCraft.WebApp.ViewModels.FormViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents;

public class FormViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FormDetailsViewModel form)
    {
        return View(form); //default
    }
}