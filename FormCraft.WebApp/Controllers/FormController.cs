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
using System.Security.Claims;

namespace FormCraft.WebApp.Controllers;

public class FormController(IFormBusiness formBusiness, UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager) : Controller
{
    private readonly IFormBusiness _formBusiness = formBusiness;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IMapper _mapper = mapper;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<FormResponseViewModel>>> List()
    {
        List<FormResponse>? formsResponse = await _formBusiness.GetAll();

        var IsAuthenticated = HttpContext.User.Identity!.IsAuthenticated;

        if (!IsAuthenticated)
        {
            formsResponse = formsResponse.Where(f => f.StatusId != StatusEnum.InProgress).ToList();
        }

        var formsVm = _mapper.Map<List<FormResponseViewModel>>(formsResponse);

        return View("Index", formsVm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        if (!HttpContext.User.Identity!.IsAuthenticated)
            return View(nameof(Index));

        var user = await _userManager.GetUserAsync(HttpContext.User);

        var request = new CreateFormRequest("Form Title", FormTypeEnum.Survey, user!.Id);

        var form = await _formBusiness.Create(request);

        var formViewModel = new FormViewModel()
        {
            CreateFormModel = _mapper.Map<CreateFormModel>(form),
            Questions = []
        };

        return View(formViewModel);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Details(string id)
    {

        try
        {
            var form = await _formBusiness.GetById(id);

            var formViewModel = new FormViewModel()
            {
                CreateFormModel = _mapper.Map<CreateFormModel>(form),
                Questions = _mapper.Map<List<QuestionDetailsViewModel>>(form.Questions)
            };

            return form.StatusId switch
            {
                StatusEnum.InProgress => View("Create", new FormViewModel()
                {
                    CreateFormModel = _mapper.Map<CreateFormModel>(form),
                    Questions = _mapper.Map<List<QuestionDetailsViewModel>>(form.Questions)
                }),
                StatusEnum.Validated => View(_mapper.Map<FormDetailsViewModel>(form)),
                StatusEnum.Closed => View(_mapper.Map<FormDetailsViewModel>(form)),
                _ => View(nameof(Index))
            };
            ViewBag.NumberOfSounders = _formBusiness.SounderCount(id);

            ViewBag.NumberOfSounders = _formBusiness.SounderCount(id);

            return View(formVm);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<FormResponseViewModel>>> Search(
        string? Label,
        string? CurrentUserId,
        string[] IsStatusEnumPIcked,
        string[] IsFormTypePicked,
        int? Order
        )
    {



        var Id = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
        CurrentUserId = Id;

        var request = new SearchFormRequest()
        {
            CurrentUserId = CurrentUserId,
            IsFormTypePicked = IsFormTypePicked,
            Order = Order,
            IsStatusEnumPicked = IsStatusEnumPIcked,
            Label = Label
        };

        var searchResult = await _formBusiness.Search(request);

        var IsAuthenticated = HttpContext.User.Identity!.IsAuthenticated;
        if (!IsAuthenticated)
        {
            searchResult = searchResult.Where(f => f.StatusId != StatusEnum.InProgress).ToList();
        }

        var resultList = _mapper.Map<List<FormResponseViewModel>>(searchResult);


        return ViewComponent("FormRows", resultList);
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

        if (!HttpContext.User.Identity!.IsAuthenticated)
            throw new BadRequestException("Delete not authorize");

        var user = await _userManager.GetUserAsync(HttpContext.User);

        try
        {
            await _formBusiness.Delete(request, user!.Id, User.IsInRole("Admin"));
            return NoContent();
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

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<FormResponse>> Duplicate(string id)
    {
        var creatorId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;

        var duplicatedForm = await _formBusiness.Duplicate(id, creatorId!);

        return Ok(duplicatedForm);
    }
}
