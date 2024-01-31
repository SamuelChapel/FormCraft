using AutoMapper;
using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Entities;
using FormCraft.WebApp.Models;
using FormCraft.WebApp.ViewModels;
using FormCraft.WebApp.ViewModels.FormViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.Controllers;

public class FormController(IFormBusiness formBusiness, UserManager<AppUser> userManager, IMapper mapper) : Controller
{
    private readonly IFormBusiness _formBusiness = formBusiness;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<FormResponse>>> Index()
    {
        var formsResponse = await _formBusiness.GetAll();
        var formsVm = _mapper.Map<FormIndexViewModel>(formsResponse);

        return View(formsVm);
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
    public async Task<ActionResult<FormResponse>> Details(string id)
    {
        try
        {
            var reponse = await _formBusiness.GetById(id);
            var formVm = _mapper.Map<FormDetailsViewModel>(reponse);

            return View(formVm);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("Search")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<SearchFormResponse>>> Search(SearchFormRequest request)
    {
        var Id = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
        request = request with { CurrentUserId = Id };

        var searchResult = await _formBusiness.Search(request);
        var formsVm = _mapper.Map<FormSearchViewModel>(searchResult);


        if (searchResult.Count > 1)
            return RedirectToAction(nameof(Index), formsVm);

        else if (searchResult.Count == 1)
            return RedirectToAction(nameof(Details), formsVm);

        else
            return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<FormResponse>> Create(CreateFormRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);


        var user = await _formBusiness.Create(request);

        return RedirectToAction(nameof(Details), new { id = user.Id });
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<FormResponse>> Update(UpdateFormRequest request)
    {
        try
        {
            var formReponse = await _formBusiness.Update(request);

            return RedirectToAction(nameof(Index), new { id = formReponse.Id });
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

    [HttpPost] // POST rather than DELETE
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(DeleteFormRequest request)
    {
        try
        {
            await _formBusiness.Delete(request);
            return RedirectToAction(nameof(Index));
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
