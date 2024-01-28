using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Responses.Answer;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswerController(IAnswerBusiness answerBusiness) : ControllerBase
{
    private readonly IAnswerBusiness _answerBusiness = answerBusiness;

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<AnswerResponse>> GetById(string id)
    {
        var answer = await _answerBusiness.GetById(id);

        return Ok(answer);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<AnswerResponse>>> GetAll()
    {
        var answers = await _answerBusiness.GetAll();

        return Ok(answers);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<AnswerResponse>> Create([FromBody] CreateAnswerRequest request)
    {
        var answer = await _answerBusiness.Create(request);

        return CreatedAtAction(nameof(GetById), new { Id = answer.Id }, answer);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<AnswerResponse>> Update(UpdateAnswerRequest request)
    {
        var answer = await _answerBusiness.Update(request);

        return Ok(answer);
    }

    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(DeleteAnswerRequest id)
    {
        await _answerBusiness.Delete(id);

        return NoContent();
    }
}
