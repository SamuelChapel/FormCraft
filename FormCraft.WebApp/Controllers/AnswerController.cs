using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Answer;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.Controllers;

public class AnswerController : Controller
{
    private readonly IAnswerBusiness _answerBusiness;

    public AnswerController(IAnswerBusiness answerBusiness)
    {
        _answerBusiness = answerBusiness;
    }

    public async Task<ActionResult> Details(string id)
    {
        return View(await _answerBusiness.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateAnswerRequest request)
    {
        var answer = await _answerBusiness.Create(request);

        var answerViewComponent = ViewComponent("CreateAnswer", answer);

        return answerViewComponent;
    }

    [HttpPost]
    public async Task<ActionResult> Update(UpdateAnswerRequest request)
    {
        try
        {
            return Json(await _answerBusiness.Update(request));
        }
        catch
        {
            return View();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string Id)
    {
        try
        {
            await _answerBusiness.Delete(new DeleteAnswerRequest(Id));
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
