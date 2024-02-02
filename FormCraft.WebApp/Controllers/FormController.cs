using AutoMapper;
using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Entities;
using FormCraft.WebApp.Models;
using FormCraft.WebApp.ViewModels;
using FormCraft.WebApp.ViewModels.FormViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
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
public async Task<ActionResult<List<FormResponseViewModel>>> List()
    {
        await Console.Out.WriteLineAsync("Action called");

        List<FormResponse>? formsResponse = await _formBusiness.GetAll();
        var formsVm = _mapper.Map<List<FormResponseViewModel>>(formsResponse);

        return View("Index", formsVm);
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
    public async Task<ActionResult<List<FormResponseViewModel>>> Search([FromBody] SearchFormRequest request)
    {
        var Id = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
        request.CurrentUserId = Id;

        var searchResult = await _formBusiness.Search(request);

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
        try
        {
            await _formBusiness.Delete(request);
            //return RedirectToAction(nameof(List));
            return NoContent();
            ;
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
