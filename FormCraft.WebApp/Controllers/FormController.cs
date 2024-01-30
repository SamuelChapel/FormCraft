using FormCraft.Business.Contracts;
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
    public async Task<IActionResult> Details(string id)
    {
        var form = await _formBusiness.GetById(id);

        if (form is null)
            return RedirectToAction(nameof(Index));

        return View(form);
    }
}
