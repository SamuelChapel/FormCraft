using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Contracts.Responses.Form;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormController(IFormBusiness formBusiness) : ControllerBase
{
    private readonly IFormBusiness _formBusiness = formBusiness;

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<FormWithQuestionsResponse>> GetById(string id)
    {
        var form = await _formBusiness.GetById(id);

        return Ok(form);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<List<FormResponse>>> GetAll()
    {
        var forms = await _formBusiness.GetAll();

        return Ok(forms);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<FormResponse>> Create([FromBody] CreateFormRequest request)
    {
        var form = await _formBusiness.Create(request);

        return CreatedAtAction(nameof(GetById), new { form.Id }, form);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<FormResponse>> Update(UpdateFormRequest request)
    {
        var form = await _formBusiness.Update(request);

        return Ok(form);
    }

    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(DeleteFormRequest id)
    {
        await _formBusiness.Delete(id);

        return NoContent();
    }
}
