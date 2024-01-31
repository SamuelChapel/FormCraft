using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;
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

        var request = new CreateFormRequest("Form Title", FormTypeEnum.Survey, user!.Id);

        var form = await _formBusiness.Create(request);

        var formViewModel = new FormViewModel()
        {
            CreateFormModel = new CreateFormModel()
            {
                Id = form.Id,
                CreatorId = user!.Id,
                FormTypeId = form.FormTypeId,
                Label = form.Label,
                StatusId = StatusEnum.InProgress
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

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<FormResponse>> GetById(string id)
    {
        try
        {
            var reponse = await _formBusiness.GetById(id);
            return View(reponse); //Vue à créer
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<FormResponse>>> GetAll()
    {
        var formsResponse = await _formBusiness.GetAll();

        return View(formsResponse); //Vue à créer
    }

    [HttpPost("Search")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<SearchFormResponse>>> Search(SearchFormRequest request)
    {
        var searchResult = await _formBusiness.Search(request);

        return View(searchResult); //A voir
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<FormResponse>> Update(UpdateFormRequest request)
    {
        try
        {
            var formReponse = await _formBusiness.Update(request);

            return RedirectToAction(nameof(GetById), new { id = formReponse.Id });
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost] // POST Rather than DELETE
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(DeleteFormRequest request)
    {
        try
        {
            await _formBusiness.Delete(request);
            return RedirectToAction(nameof(GetAll));
            //return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
