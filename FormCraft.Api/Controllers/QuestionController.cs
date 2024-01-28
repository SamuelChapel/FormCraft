using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.Business.Contracts.Responses.Question;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController(IQuestionService questionService) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<QuestionResponse>> GetById(string id)
    {
        var question = await _questionService.GetById(id);

        return Ok(question);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<QuestionResponse>>> GetAll()
    {
        var questions = await _questionService.GetAll();

        return Ok(questions);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<QuestionResponse>> Create([FromBody] CreateQuestionRequest request)
    {
        var question = await _questionService.Create(request);

        return CreatedAtAction(nameof(GetById), new { question.Id }, question);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<QuestionResponse>> Update(UpdateQuestionRequest request)
    {
        var question = await _questionService.Update(request);

        return Ok(question);
    }

    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(DeleteQuestionRequest id)
    {
        await _questionService.Delete(id);

        return NoContent();
    }
}
