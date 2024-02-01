using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.Controllers;

public class FormController(IFormBusiness formBusiness) : Controller
{
    private readonly IFormBusiness _formBusiness = formBusiness;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var forms = await _formBusiness.GetAll();

        return View(forms);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new FormViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(FormViewModel formViewModel)
    {
        var request = new CreateFormRequest(formViewModel.CreateFormModel.Label, formViewModel.CreateFormModel.FormType);

        var result =  await _formBusiness.Create(request);

        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var form = await _formBusiness.GetById(id);

        if (form is null)
            return RedirectToAction(nameof(Index));

        return View(form);
    }
}
