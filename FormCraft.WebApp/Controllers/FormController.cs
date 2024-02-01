using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Entities;
using FormCraft.WebApp.Models;
using FormCraft.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.Controllers;

public class FormController(IFormBusiness formBusiness, UserManager<AppUser> userManager) : Controller
{
    private readonly IFormBusiness _formBusiness = formBusiness;
    private readonly UserManager<AppUser> _userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var forms = await _formBusiness.GetAll();

        return View(forms);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        var request = new CreateFormRequest("Form Title", Entities.FormTypeEnum.Survey, user!.Id);

        var form = await _formBusiness.Create(request);

        var formViewModel = new FormViewModel()
        {
            CreateFormModel = new CreateFormModel()
            {
                Id = form.Id,
                CreatorId = user!.Id,
                FormTypeId = form.FormTypeId,
                Label = form.Label,
                StatusId = Entities.StatusEnum.InProgress
            },
            Questions = []
        };

        return View(formViewModel);
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
